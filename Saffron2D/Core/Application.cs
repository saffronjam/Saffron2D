using Saffron2D.Exceptions;
using System.Collections.Generic;
using Saffron2D.GuiCollection;

using SFML.Graphics;
using SFML.System;

namespace Saffron2D.Core
{
    public abstract class Application
    {
        public Window Window { get; }

        private static Application _instance = null;

        private readonly List<Layer> _layers = new List<Layer>();
        private bool _shouldRun = true;

        protected Application(SFML.Window.VideoMode videoMode, string windowTitle)
        {
            if (_instance != null)
            {
                throw new SaffronStateException("Application already created.");
            }

            _instance = this;

            Window = new Window(videoMode, windowTitle);
            Window.Closed += (sender, args) => { Exit(); };

            Input.AddEventSource(Window);

            Gui.OnInit();
        }

        private void RenderGui(Time dt)
        {
            Gui.OnUpdate(dt);
                
            foreach (var layer in _layers)
            {
                layer.OnGuiRender();
            }

            Gui.OnRender();
        }

        public void Start()
        {
            OnInit();

            while (_shouldRun)
            {
                var dt = Global.Clock.Restart();

                Window.DispatchEvents();

                Window.Clear(Color.Black);
                foreach (var layer in _layers)
                {
                    layer.OnUpdate(dt);
                }

                RenderGui(dt);
                
                Input.OnUpdate();
                Run.OnUpdate(dt);
                Run.Execute();
                
                Window.Display();
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
            _layers[_layers.Count - 1].OnDetach();
            _layers.RemoveAt(_layers.Count - 1);
        }

        public void RemoveLayer(Layer layer)
        {
            layer.OnDetach();
            _layers.Remove(layer);
        }
    }
}