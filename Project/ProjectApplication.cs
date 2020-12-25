using System;
using ImGuiNET;
using Project.Layers;
using Saffron2D.Core;
using SFML.Window;

namespace Project
{
    public class ProjectApplication : Application
    {

        public ProjectApplication() : base(new VideoMode(1500, 800), "Saffron2D")
        {
        }

        public override void OnInit()
        {
            PushLayer(new MainLayer());
        }
    }

}