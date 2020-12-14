using System;
using ImGuiNET;
using Saffron2D.Core;
using Saffron2D.GuiCollection;
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
            ImGui.Begin("TESt");
            ImGui.End();
        }
    }
}