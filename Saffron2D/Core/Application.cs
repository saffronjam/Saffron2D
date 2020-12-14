using System;
using Saffron2D.Exceptions;
using System.Collections.Generic;
using ImGuiNET;
using Saffron2D.GuiCollection;
using SFML.Graphics;

namespace Saffron2D.Core
{
    public abstract class Application
    {
        public Window Window { get; }

        private static Application instance = null;

        private readonly List<Layer> layers = new List<Layer>();
        private bool shouldRun = true;

        protected Application(SFML.Window.VideoMode videoMode, string windowTitle)
        {
            if (instance != null)
            {
                throw new SaffronStateException("Application already created.");
            }

            instance = this;

            Window = new Window(videoMode, windowTitle);
            Window.Closed += (sender, args) => { Exit(); };

            Input.AddEventSource(Window);

            Gui.Init();
        }

        private void RenderGui()
        {
            Gui.Begin();

            foreach (var layer in layers)
            {
                layer.OnGuiRender();
            }

            Gui.End();
        }

        public void Start()
        {
            OnInit();

            while (shouldRun)
            {
                var dt = Global.Clock.Restart();

                Window.DispatchEvents();

                Window.Clear(Color.Black);
                foreach (var layer in layers)
                {
                    layer.OnUpdate(dt);
                }

                RenderGui();

                Input.OnUpdate();
                Run.OnUpdate(dt);
                Run.Execute();
                Window.Display();
            }

            OnShutdown();
        }

        public virtual void Exit()
        {
            shouldRun = false;
        }

        public virtual void OnInit()
        {
        }

        public virtual void OnShutdown()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public static Application Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                throw new SaffronStateException("Application not created.");
            }
        }

        public void PushLayer(Layer layer)
        {
            layers.Add(layer);
            layer.OnAttach();
        }

        public void PopLayer()
        {
            layers[layers.Count - 1].OnDetach();
            layers.RemoveAt(layers.Count - 1);
        }

        public void RemoveLayer(Layer layer)
        {
            layer.OnDetach();
            layers.Remove(layer);
        }
    }
}