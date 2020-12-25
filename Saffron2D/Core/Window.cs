using System;
using SFML.Graphics;
using SFML.Window;

namespace Saffron2D.Core
{
    public class Window
    {
        private string _title;

        public SFML.Graphics.RenderWindow NativeWindow { get; }

        public string Title
        {
            get => _title;
            set
            {
                if (Title == value) return;
                _title = value;
                NativeWindow.SetTitle(value);
            }
        }

        public Window(VideoMode videoMode, string title)
        {
            NativeWindow = new SFML.Graphics.RenderWindow(videoMode, title);
            Title = title;
            NativeWindow.SetVerticalSyncEnabled(true);
        }

        public void DispatchEvents()
        {
            NativeWindow.DispatchEvents();
        }

        public void Clear(Color color)
        {
            NativeWindow.Clear(color);
        }
        
        public void Display()
        {
            NativeWindow.Display();
        }

        public event EventHandler Closed
        {
            add
            {
                lock (this)
                {
                    NativeWindow.Closed += value;
                }
            }
            remove
            {
                lock (this)
                {
                    NativeWindow.Closed -= value;
                }
            }
        }

        public event EventHandler<SizeEventArgs> Resized
        {
            add
            {
                lock (this)
                {
                    NativeWindow.Resized += value;
                }
            }
            remove
            {
                lock (this)
                {
                    NativeWindow.Resized -= value;
                }
            }
        }

        public event EventHandler LostFocus
        {
            add
            {
                lock (this)
                {
                    NativeWindow.LostFocus += value;
                }
            }
            remove
            {
                lock (this)
                {
                    NativeWindow.LostFocus -= value;
                }
            }
        }

        public event EventHandler GainedFocus
        {
            add
            {
                lock (this)
                {
                    NativeWindow.GainedFocus += value;
                }
            }
            remove
            {
                lock (this)
                {
                    NativeWindow.GainedFocus -= value;
                }
            }
        }
    }
}