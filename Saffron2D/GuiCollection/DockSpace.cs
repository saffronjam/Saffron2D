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

            ImGuiWindowFlags host_window_flags = 0;
            host_window_flags |= ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoDocking;
            host_window_flags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;
            host_window_flags |= ImGuiWindowFlags.NoBackground;

            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0.0f, 0.0f));
            ImGui.Begin("DockSpaceViewport_%08X", host_window_flags);
            ImGui.PopStyleVar(3);

            var dockspace_id = ImGui.GetID("DockSpace");
            ImGui.DockSpace(dockspace_id, new Vector2(0.0f, 0.0f), ImGuiDockNodeFlags.None);
            ImGui.End();
        }
    }
}