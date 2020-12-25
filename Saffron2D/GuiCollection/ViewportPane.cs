using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Saffron2D.GuiCollection
{
    public class ViewportPane
    {
        private readonly string _viewportTitle;
        private static Texture _fallbackTexture;
        private uint _dockId;

        private Vector2f _topLeft;
        private Vector2f _bottomRight;

        private readonly uint _inactiveBorderColor = BitConverter.ToUInt32(new byte[] {255, 140, 0, 80});
        private readonly uint _activeBorderColor = BitConverter.ToUInt32(new byte[] {255, 140, 0, 180});

        public ViewportPane(string viewportTitle, RenderTexture target)
        {
            this._viewportTitle = viewportTitle;
            // One pixel dark grey texture
            var image = new Image(1, 1);
            image.Pixels[0] = 50;
            image.Pixels[1] = 50;
            image.Pixels[2] = 50;
            image.Pixels[3] = 255;

            _fallbackTexture = new Texture(image);
            Target = target;

            TopLeft = new Vector2f(0, 0);
            BottomRight = new Vector2f(0, 0);
        }

        public void OnGuiRender()
        {
            var tl = TopLeft;
            var br = BottomRight;

            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);

            const int uuid = 0;

            var builder = new StringBuilder();
            builder.Append(_viewportTitle).Append("##").Append(uuid);

            ImGui.Begin(builder.ToString(), ImGuiWindowFlags.NoFocusOnAppearing);

            _dockId = ImGui.GetWindowDockID();

            Hovered = ImGui.IsWindowHovered();
            Focused = ImGui.IsWindowFocused();

            var viewportOffset = ImGui.GetCursorPos(); // includes tab bar
            var minBound = ImGui.GetWindowPos();
            minBound.X += viewportOffset.X;
            minBound.Y += viewportOffset.Y;

            var windowSize = ImGui.GetWindowSize();
            var maxBound = new Vector2(minBound.X + windowSize.X - viewportOffset.X,
                minBound.Y + windowSize.Y - viewportOffset.Y);
            _topLeft.X = minBound.X;
            _topLeft.Y = minBound.Y;
            _bottomRight.X = maxBound.X;
            _bottomRight.Y = maxBound.Y;

            var vpSize = ViewportSize;
            var imageRendererId = Target?.Texture.NativeHandle ?? _fallbackTexture.NativeHandle;
            ImGui.Image((IntPtr) imageRendererId, new Vector2(vpSize.X, vpSize.Y));

            ImGui.GetWindowDrawList().AddRect(new Vector2(TopLeft.X, tl.Y), new Vector2(br.X, br.Y),
                Focused ? _activeBorderColor : _inactiveBorderColor, 0.0f, ImDrawCornerFlags.All, 4);

            ImGui.End();
            ImGui.PopStyleVar();

            if (Target == null)
            {
                return;
            }

            var candidateSize = new Vector2u((uint) ViewportSize.X, (uint) ViewportSize.Y);
            if (candidateSize == Target.Size || WantRenderTargetResize == null) return;
            
            var sizeEvent = new SizeEvent {Width = candidateSize.X, Height = candidateSize.Y};
            WantRenderTargetResize(this, new SizeEventArgs(sizeEvent));
        }


        public RenderTexture Target { get; set; }

        public Vector2f TopLeft
        {
            get => _topLeft;
            private set => _topLeft = value;
        }

        public Vector2f BottomRight
        {
            get => _bottomRight;
            private set => _bottomRight = value;
        }

        public bool Hovered { get; private set; }
        public bool Focused { get; private set; }

        public Vector2f ViewportSize => BottomRight - TopLeft;

        public event EventHandler<SizeEventArgs> WantRenderTargetResize = null;
    }
}