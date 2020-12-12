using System;
using SFML.Window;

namespace Saffron2D.Core
{
    public class Window
    {
        private string _Title;

        public SFML.Window.Window NativeWindow { get; }

        public string Title
        {
            get => _Title;
            set
            {
                if (Title == value) return;
                _Title = value;
                NativeWindow.SetTitle(value);
            }
        }

        public Window(VideoMode videoMode, string title)
        {
            NativeWindow = new SFML.Window.Window(videoMode, title);
            Title = title;
            NativeWindow.SetVerticalSyncEnabled(true);
        }

        public void DispatchEvents()
        {
            NativeWindow.DispatchEvents();
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