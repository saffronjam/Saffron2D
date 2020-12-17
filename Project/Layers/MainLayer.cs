using System.Numerics;
using ImGuiNET;
using Saffron2D.Core;
using Saffron2D.Graphics;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Time = SFML.System.Time;
using Window = SFML.Window.Window;

namespace Project.Layers
{
    public class MainLayer : Layer
    {
        private Camera _camera;
        private ControllableRenderTarget _target;
        private Scene _scene;

        // Gui
        private DockSpace _dockSpace;
        private ViewportPane _viewportPane;

        public override void OnAttach()
        {
            _camera = new Camera();
            _target = new ControllableRenderTarget(new RenderTexture(10, 10), Color.Black);
            _scene = new Scene(_target.RenderTarget, _camera);

            // Gui
            _dockSpace = new DockSpace();
            _viewportPane = new ViewportPane("Scene", _target.RenderTarget as RenderTexture);
            _viewportPane.WantRenderTargetResize += OnRenderTargetResize;

            RenderTargetManager.Add(_target);
            Application.Instance.Scene = _scene;
        }

        public override void OnUpdate(Time dt)
        {
            _scene.OnUpdate();

            var rect = new RectangleShape(new Vector2f(10, 100));
            _scene.Submit(rect);
        }

        public override void OnGuiRender()
        {
            _dockSpace.OnGuiRender();
            Application.Instance.OnGuiRender();
            _viewportPane.OnGuiRender();
            _camera.OnGuiRender();

            if (ImGui.Begin("Project"))
            {
                Gui.BeginPropertyGrid();
                Gui.EndPropertyGrid();
                ImGui.End();
            }

            ImGui.ShowDemoWindow();
        }

        private void OnRenderTargetResize(object sender, SizeEventArgs args)
        {
            var renderTarget = new RenderTexture(args.Width, args.Height);
            _target.RenderTarget = renderTarget;
            _viewportPane.Target = renderTarget;
            _scene.Target = renderTarget;
            _scene.ViewportSize = new Vector2f(args.Width, args.Height);
        }
    }
}