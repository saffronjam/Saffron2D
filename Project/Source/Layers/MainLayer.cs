using System;
using Saffron2D.Core;
using SFML.Window;

using Time = SFML.System.Time;

namespace Project.Layers
{
    public class MainLayer : Layer
    {
        public override void OnAttach()
        {
        }

        public override void OnUpdate(Time dt)
        {
            if (Input.IsMouseButtonPressed(Mouse.Button.Left))
            {
                Run.Interval(() => Application.Instance.Window.Title = "Delta time: " + dt.AsMicroseconds(), Time.FromSeconds(1));
            }
        }
    }
}