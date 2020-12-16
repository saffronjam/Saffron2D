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
        private Camera camera;
        private ControllableRenderTarget target;
        private Scene scene;
        
        // Gui
        private DockSpace dockSpace;
        private ViewportPane viewportPane;

        public override void OnAttach()
        {
            camera = new Camera();
            target = new ControllableRenderTarget(new RenderTexture(10, 10), Color.Black);
            scene = new Scene(target.RenderTarget, camera);
            
            // Gui
            dockSpace = new DockSpace();
            viewportPane = new ViewportPane("Scene", target.RenderTarget as RenderTexture);
            viewportPane.WantRenderTargetResize += OnRenderTargetResize;

            RenderTargetManager.Add(target);
            Application.Instance.Scene = scene;
        }

        public override void OnUpdate(Time dt)
        {
            scene.OnUpdate();

            var rect = new RectangleShape(new Vector2f(10, 100));
            scene.Submit(rect);
        }

        public override void OnGuiRender()
        {
            dockSpace.OnGuiRender();
            Application.Instance.OnGuiRender();
            viewportPane.OnGuiRender();
            
            ImGui.ShowDemoWindow();
        }

        private void OnRenderTargetResize(object sender, SizeEventArgs args)
        {
            var renderTarget = new RenderTexture(args.Width, args.Height);
            target.RenderTarget = renderTarget;
            viewportPane.Target = renderTarget;
            scene.Target = renderTarget;
            scene.ViewportSize = new Vector2f(args.Width, args.Height);
        }
    }
}