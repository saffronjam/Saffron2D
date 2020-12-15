using System;
using System.Numerics;
using ImGuiNET;
using Saffron2D.Core;
using Saffron2D.GuiCollection;
using SFML.Graphics;
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
            
        }

        public override void OnGuiRender()
        {
            ImGui.Begin("MY WINDOW");
            ImGui.DockSpace(0);
            ImGui.Begin("TEST1");
            ImGui.End();
            ImGui.Begin("TEST2");
            ImGui.End();
            ImGui.Begin("TEST3");
            ImGui.End();
            ImGui.End();
        }
    }
}