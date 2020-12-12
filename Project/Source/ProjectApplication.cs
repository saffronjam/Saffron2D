using System;
using Project.Layers;
using Saffron2D.Core;
using SFML.Window;

namespace Project
{
    public class ProjectApplication : Application
    {
        public ProjectApplication() : base(new VideoMode(1024, 720), "Saffron2D")
        {
            instance = this;
        }

        public override void OnInit()
        {
            PushLayer(new MainLayer());
        }
    }

}