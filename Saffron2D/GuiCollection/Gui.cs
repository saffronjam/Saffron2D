using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;
using Saffron2D.Core;
using SFML.Graphics;
using SFML.System;

namespace Saffron2D.GuiCollection
{
    public class Gui
    {
        private static Dictionary<int, ImFontPtr> fonts = new Dictionary<int, ImFontPtr>();
        private static Tuple<int, ImFontPtr> activeFont;

        public static void Init()
        {
            // Setup Platform/Renderer bindings
            var window = Application.Instance.Window;
            GuiImpl.Sfml.Init( window.NativeWindow as RenderWindow, true);
            
            var io = ImGui.GetIO();

            const int defaultFontSize = 18;
            var newFont = AddFont("Resources/Assets/Fonts/segoeui.ttf", defaultFontSize);

            AddFont("Resources/Assets/Fonts/segoeui.ttf", 8);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 12);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 14);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 20);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 22);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 24);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 28);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 32);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 56);
            AddFont("Resources/Assets/Fonts/segoeui.ttf", 72);

            ImGui.StyleColorsDark();
            var style = ImGui.GetStyle();
            if ((io.ConfigFlags | ImGuiConfigFlags.ViewportsEnable) > 0x00)
            {
                style.WindowRounding = 0.0f;
            }

            // Main Blue
            //var mainVibrant = { 0.26f, 0.59f, 0.98f };
            //var mainVibrantDark = { 0.24f, 0.52f, 0.88f };
            //var mainLessVibrant = { 0.06f, 0.53f, 0.98f 

            // Main Orange
            //var mainVibrant = { 0.89f, 0.46f, 0.16f };
            //var mainVibrantDark = { 0.79f, 0.38f, 0.14f };
            //var mainLessVibrant = { 0.89f, 0.39f, 0.02f };

            // Main Purple
            var mainVibrant = new Vector3f(0.29f, 0.13f, 0.42f);
            var mainVibrantDark = new Vector3f(0.19f, 0.15f, 0.23f);
            var mainLessVibrant = new Vector3f(0.33f, 0.18f, 0.48f);

            var mainNoTint = new Vector4f(mainVibrant, 1.00f); //3	Main no tint
            var mainTint1 = new Vector4f(mainVibrant, 0.95f); //9	Main tinted1
            var mainTint2 = new Vector4f(mainVibrant, 0.80f); //8	Main tinted2
            var mainTint3 = new Vector4f(mainVibrant, 0.67f); //2	Main tinted3
            var mainTint4 = new Vector4f(mainVibrant, 0.40f); //1	Main tinted4
            var mainTint5 = new Vector4f(mainVibrant, 0.35f); //13	Main tinted5

            var mainDark = new Vector4f(mainVibrantDark, 1.00f); //4	Main dark1 no tint

            var mainLessVibrantNoTint = new Vector4f(mainLessVibrant, 1.00f); //6	Less blue no tint
            var mainLessVibrantTint1 = new Vector4f(mainLessVibrant, 0.60f); //14 Less blue tinted1

            var coMain = new Vector4f(1.00f, 0.43f, 0.35f, 1.00f); //10	2ndMain no tint
            var coMainDark = new Vector4f(0.90f, 0.70f, 0.00f, 1.00f); //11	3rdMain no tint
            var coMainRed = new Vector4f(1.00f, 0.60f, 0.00f, 1.00f); //12	Co3rdMain 
            //
            // static Vector4 ToImVec4(Vector4f vector)
            // {
            // 	return new Vector4(vector.X, vector.Y, vector.Z, vector.W );
            // };

            style.Alpha = 1.0f;
            style.FrameRounding = 3.0f;

            /*
            
            Text,
            TextDisabled,
            WindowBg,
            ChildBg,
            PopupBg,
            Border,
            BorderShadow,
            FrameBg,
            FrameBgHovered,
            FrameBgActive,
            TitleBg,
            TitleBgActive,
            TitleBgCollapsed,
            MenuBarBg,
            ScrollbarBg,
            ScrollbarGrab,
            ScrollbarGrabHovered,
            ScrollbarGrabActive,
            CheckMark,
            SliderGrab,
            SliderGrabActive,
            Button,
            ButtonHovered,
            ButtonActive,
            Header,
            HeaderHovered,
            HeaderActive,
            Separator,
            SeparatorHovered,
            SeparatorActive,
            ResizeGrip,
            ResizeGripHovered,
            ResizeGripActive,
            Tab,
            TabHovered,
            TabActive,
            TabUnfocused,
            TabUnfocusedActive,
            DockingPreview,
            DockingEmptyBg,
            PlotLines,
            PlotLinesHovered,
            PlotHistogram,
            PlotHistogramHovered,
            TextSelectedBg,
            DragDropTarget,
            NavHighlight,
            NavWindowingHighlight,
            NavWindowingDimBg,
            ModalWindowDimBg,
            
            */


            // style.C[ImGuiCol.Text] = ImVec4(0.00f, 0.00f, 0.00f, 1.00f);
            // style.Colors[ImGuiCol.TextDisabled] = ImVec4(0.60f, 0.60f, 0.60f, 1.00f);
            // style.Colors[ImGuiCol.WindowBg] = ImVec4(0.94f, 0.94f, 0.94f, 0.94f);
            // style.Colors[ImGuiCol.PopupBg] = ImVec4(1.00f, 1.00f, 1.00f, 0.94f);
            // style.Colors[ImGuiCol.Border] = ImVec4(0.00f, 0.00f, 0.00f, 0.39f);
            // style.Colors[ImGuiCol.BorderShadow] = ImVec4(1.00f, 1.00f, 1.00f, 0.10f);
            // style.Colors[ImGuiCol.FrameBg] = ImVec4(1.00f, 1.00f, 1.00f, 0.94f);
            // style.Colors[ImGuiCol.FrameBgHovered] = ToImVec4(mainTint4);									//1
            // style.Colors[ImGuiCol.FrameBgActive] = ToImVec4(mainTint3);										//2
            // style.Colors[ImGuiCol.TitleBg] = ImVec4(0.96f, 0.96f, 0.96f, 1.00f);
            // style.Colors[ImGuiCol.TitleBgCollapsed] = ImVec4(0.96f, 0.96f, 0.96f, 1.00f);
            // style.Colors[ImGuiCol.TitleBgActive] = ImVec4(0.96f, 0.96f, 0.96f, 1.00f);
            // style.Colors[ImGuiCol.MenuBarBg] = ImVec4(0.86f, 0.86f, 0.86f, 1.00f);
            // style.Colors[ImGuiCol.ScrollbarBg] = ImVec4(0.98f, 0.98f, 0.98f, 0.53f);
            // style.Colors[ImGuiCol.ScrollbarGrab] = ImVec4(0.69f, 0.69f, 0.69f, 1.00f);
            // style.Colors[ImGuiCol.ScrollbarGrabHovered] = ImVec4(0.59f, 0.59f, 0.59f, 1.00f);
            // style.Colors[ImGuiCol.ScrollbarGrabActive] = ImVec4(0.49f, 0.49f, 0.49f, 1.00f);
            // style.Colors[ImGuiCol.CheckMark] = ToImVec4(mainNoTint);										//3
            // style.Colors[ImGuiCol.SliderGrab] = ToImVec4(mainDark);											//4
            // style.Colors[ImGuiCol.SliderGrabActive] = ToImVec4(mainDark);									//4
            // style.Colors[ImGuiCol.Button] = ToImVec4(mainTint2);											//1
            // style.Colors[ImGuiCol.ButtonHovered] = ToImVec4(mainNoTint);									//3
            // style.Colors[ImGuiCol.ButtonActive] = ToImVec4(mainLessVibrantNoTint);							//6
            // style.Colors[ImGuiCol.Header] = ToImVec4(mainTint4);											//7
            // style.Colors[ImGuiCol.HeaderHovered] = ToImVec4(mainTint2);										//8
            // style.Colors[ImGuiCol.HeaderActive] = ToImVec4(mainNoTint);										//3
            // style.Colors[ImGuiCol.ResizeGrip] = ImVec4(1.00f, 1.00f, 1.00f, 0.50f);
            // style.Colors[ImGuiCol_ResizeGripHovered] = ToImVec4(mainTint3);									//2
            // style.Colors[ImGuiCol_ResizeGripActive] = ToImVec4(mainTint1);									//9
            // style.Colors[ImGuiCol_PlotLines] = ImVec4(0.39f, 0.39f, 0.39f, 1.00f);
            // style.Colors[ImGuiCol_PlotLinesHovered] = ToImVec4(coMain);										//10
            // style.Colors[ImGuiCol_PlotHistogram] = ToImVec4(coMainDark);									//11
            // style.Colors[ImGuiCol_PlotHistogramHovered] = ToImVec4(coMainRed);								//12
            // style.Colors[ImGuiCol_TextSelectedBg] = ToImVec4(mainTint5);									//13
            // style.Colors[ImGuiCol_ModalWindowDarkening] = ImVec4(0.20f, 0.20f, 0.20f, 0.35f);
            // style.Colors[ImGuiCol_Tab] = ToImVec4(mainTint4);												//1
            // style.Colors[ImGuiCol_TabHovered] = ToImVec4(mainNoTint);										//3
            // style.Colors[ImGuiCol_TabActive] = ToImVec4(mainLessVibrantNoTint);								//6
            // style.Colors[ImGuiCol_TabUnfocused] = ToImVec4(mainTint4);										//1
            // style.Colors[ImGuiCol_TabUnfocusedActive] = ToImVec4(mainLessVibrantNoTint);						//14
        }

        public static void Begin()
        {
        }

        public static void End()
        {
            var window = Application.Instance.Window;
            GuiImpl.Sfml.Render(window.NativeWindow);
        }

        public static void OnUpdate(Time dt)
        {
            var window = Application.Instance.Window;
            GuiImpl.Sfml.Update(window.NativeWindow, window.NativeWindow, dt);
        }

        private static ImFontPtr AddFont(string path, int size)
        {
            var newFont = ImGui.GetIO().Fonts.AddFontFromFileTTF(path, size);
            fonts.Add(size, newFont);
            return newFont;
        }
    }
}