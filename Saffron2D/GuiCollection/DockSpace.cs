using System.Numerics;
using ImGuiNET;

namespace Saffron2D.GuiCollection
{
    public class DockSpace
    {
        public void OnGuiRender()
        {
            var viewport = ImGui.GetMainViewport();
            
            ImGui.SetNextWindowPos(viewport.GetWorkPos());
            ImGui.SetNextWindowSize(viewport.GetWorkSize());
            ImGui.SetNextWindowViewport(viewport.ID);

            ImGuiWindowFlags hostWindowFlags = 0;
            hostWindowFlags |= ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoDocking;
            hostWindowFlags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;
            hostWindowFlags |= ImGuiWindowFlags.NoBackground;

            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0.0f, 0.0f));
            ImGui.Begin("DockSpaceViewport_%08X", hostWindowFlags);
            ImGui.PopStyleVar(3);

            var dockspaceId = ImGui.GetID("DockSpace");
            ImGui.DockSpace(dockspaceId, new Vector2(0.0f, 0.0f), ImGuiDockNodeFlags.None);
            ImGui.End();
        }
    }
}