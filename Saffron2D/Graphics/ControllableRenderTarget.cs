using Saffron2D.Core;
using SFML.Graphics;

namespace Saffron2D.Graphics
{
    public class ControllableRenderTarget
    {
        public RenderTarget RenderTarget { get; set; }
        public bool Enabled { get; set; }

        public Color ClearColor { get; set; }

        public ControllableRenderTarget(RenderTarget renderTarget, Color clearColor, bool enabled = true)
        {
            RenderTarget = renderTarget;
            ClearColor = clearColor;
            Enabled = enabled;
        }

        public ControllableRenderTarget(Window window, Color clearColor, bool enabled = true)
            : this(window.NativeWindow, clearColor, enabled)
        {
        }
    }
}