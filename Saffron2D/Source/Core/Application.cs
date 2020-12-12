using System;
using Saffron2D.Exceptions;
using System.Collections.Generic;

namespace Saffron2D.Core
{
    public abstract class Application
    {
        public Window Window { get; }

        protected static Application instance;

        private readonly List<Layer> layers = new List<Layer>();
        private bool shouldRun = true;

        protected Application(SFML.Window.VideoMode videoMode, string windowTitle)
        {
            if (instance != null)
            {
                throw new Exceptions.SaffronStateException("Application already created.");
            }

            Window = new Window(videoMode, windowTitle);
            Window.Closed += (sender, args) => { Exit(); };

            Input.AddEventSource(Window);
        }

        public void Start()
        {
            OnInit();

            while (shouldRun)
            {
                var dt = Global.Clock.Restart();

                Window.DispatchEvents();

                foreach (var layer in layers)
                {
                    layer.OnUpdate(dt);
                }

                Input.OnUpdate();
                Run.OnUpdate(dt);
                Run.Execute();
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
                throw new SaffronStateException("Application getter not implemented by client.");
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