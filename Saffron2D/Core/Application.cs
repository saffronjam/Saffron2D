using System;
using Saffron2D.Exceptions;
using System.Collections.Generic;
using ImGuiNET;
using Saffron2D.Graphics;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Saffron2D.Core
{
    public abstract class Application
    {
        public Window Window { get; }

        private static Application _instance = null;

        private readonly List<Layer> _layers = new List<Layer>();
        private bool _shouldRun = true;

        private Time _fpsTimer = Time.Zero;
        private int _cachedFps = 0;
        private Time _cachedSpf = Time.Zero;
        private Time _storedFrametime = Time.Zero;
        private int _storedFrameCount = 0;

        protected Application(SFML.Window.VideoMode videoMode, string windowTitle)
        {
            if (_instance != null)
            {
                throw new SaffronStateException("Application already created.");
            }

            _instance = this;

            Window = new Window(videoMode, windowTitle);
            Window.Closed += (sender, args) => { Exit(); };
            RenderTargetManager.Add(new ControllableRenderTarget(Window, Color.Black));

            Input.AddEventSource(Window);

            Gui.OnInit("../../../gui.ini");
        }

        public virtual void OnGuiRender()
        {
            if (ImGui.Begin("Stats"))
            {
                var dt = Global.Clock.FrameTime;
                _fpsTimer += dt;
                if (_fpsTimer.AsSeconds() < 1.0f)
                {
                    _storedFrameCount++;
                    _storedFrametime += dt;
                }
                else
                {
                    _cachedFps = (int) (_storedFrameCount / _storedFrametime.AsSeconds());
                    _cachedSpf = Time.FromSeconds(_storedFrametime.AsSeconds() / _storedFrameCount);
                    _storedFrameCount = 0;
                    _storedFrametime = Time.Zero;
                    _fpsTimer = Time.Zero;
                }
                
                Gui.BeginPropertyGrid();
                
                Gui.Property("Vendor", "SFML v.2.5.0");
                Gui.Property("Frametime", _cachedSpf.AsMicroseconds() / 1000.0f + " ms");
                Gui.Property("FPS", _cachedFps.ToString());
                
                Gui.EndPropertyGrid();
                
                ImGui.End();
                
            }
        }

        private void RenderGui(Time dt)
        {
            Gui.OnUpdate(dt);
            ImGui.PushFont(Gui.GetFont(18));

            foreach (var layer in _layers)
            {
                layer.OnGuiRender();
            }

            ImGui.PopFont();
            Gui.OnRender();
        }

        public void Start()
        {
            OnInit();

            while (_shouldRun)
            {
                var dt = Global.Clock.Restart();

                Input.OnUpdate();
                Window.DispatchEvents();

                RenderTargetManager.ClearAll();
                Scene.Begin();
                foreach (var layer in _layers)
                {
                    layer.OnUpdate(dt);
                }

                Scene.End();

                RenderGui(dt);

                Run.OnUpdate(dt);
                Run.Execute();

                RenderTargetManager.DisplayAll();
            }

            OnShutdown();
        }

        public virtual void Exit()
        {
            _shouldRun = false;
        }

        public virtual void OnInit()
        {
        }

        public virtual void OnShutdown()
        {
            Gui.OnShutdown();
        }

        public virtual void OnUpdate()
        {
        }

        public static Application Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                throw new SaffronStateException("Application not created.");
            }
        }

        public void PushLayer(Layer layer)
        {
            _layers.Add(layer);
            layer.OnAttach();
        }

        public void PopLayer()
        {
            _layers[^1].OnDetach();
            _layers.RemoveAt(_layers.Count - 1);
        }

        public void RemoveLayer(Layer layer)
        {
            layer.OnDetach();
            _layers.Remove(layer);
        }

        public Scene Scene { get; set; }
    }
}