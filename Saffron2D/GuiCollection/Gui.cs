using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using ImGuiNET;
using Saffron2D.Core;
using Saffron2D.Core.SfmlExtension;
using Saffron2D.Exceptions;
using SFML.Graphics;
using SFML.System;

namespace Saffron2D.GuiCollection
{
    public class Gui
    {
        public enum PropertyFlag
        {
            None = 0,
            Color = 1,
            Drag = 2,
            Slider = 4,
            ReadOnly = 5
        };

        private const float FloatStep = 0.1f;
        private const float FloatMin = 0.0f;
        private const float FloatMax = 10.0f;

        private const int IntStep = 1;
        private const int IntMin = 0;
        private const int IntMax = 10;

        private static bool _begunPropertyGrid = false;

        private static readonly Dictionary<int, ImFontPtr> Fonts = new Dictionary<int, ImFontPtr>();
        private static string IniFilepath { get; set; }

        public static void OnInit(string iniFilepath)
        {
            IniFilepath = iniFilepath;

            // Setup Platform/Renderer bindings
            var window = Application.Instance.Window;
            GuiImpl.Init(window.NativeWindow as RenderWindow, true);

            var io = ImGui.GetIO();

            unsafe
            {
                io.NativePtr->IniFilename = null;
            }

            ImGui.LoadIniSettingsFromDisk(IniFilepath);


            const int defaultFontSize = 18;
            var newFont = AddFont("C:/Windows/Fonts/segoeui.ttf", defaultFontSize);

            AddFont("C:/Windows/Fonts/segoeui.ttf", 8);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 12);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 14);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 20);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 22);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 24);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 28);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 32);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 56);
            AddFont("C:/Windows/Fonts/segoeui.ttf", 72);

            GuiImpl.UpdateFontTexture();


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

        public static void OnShutdown()
        {
            ImGui.SaveIniSettingsToDisk(IniFilepath);
            GuiImpl.Shutdown();
        }

        public static void OnUpdate(Time dt)
        {
            var window = Application.Instance.Window;
            GuiImpl.Update(window.NativeWindow, window.NativeWindow, dt);
        }

        public static void OnRender()
        {
            var window = Application.Instance.Window;
            GuiImpl.Render(window.NativeWindow);
        }

        private static ImFontPtr AddFont(string path, int size)
        {
            var exists = File.Exists(path);
            var newFont = ImGui.GetIO().Fonts.AddFontFromFileTTF(path, size);
            Fonts.Add(size, newFont);
            return newFont;
        }

        public static void SetFontSize(int size)
        {
            throw new NotImplementedException();
        }

        public static ImFontPtr GetFont(int size)
        {
            return GetAppropriateFont(size);
        }

        public static Texture GetFontTexture()
        {
            return GuiImpl.GetFontTexture();
        }

        private static ImFontPtr GetAppropriateFont(int size)
        {
            ImFontPtr candidate = null;
            var bestDiff = int.MaxValue;
            foreach (var (key, value) in Fonts)
            {
                var diff = Math.Abs(key - size);
                if (diff > bestDiff)
                {
                    break;
                }

                bestDiff = diff;
                candidate = value;
            }

            return candidate;
        }

        static void PushID()
        {
        }

        static void PopID()
        {
        }


        /*
         * Gui Properties
         */


        public static void BeginPropertyGrid(float width = -1.0f)
        {
            if (_begunPropertyGrid)
            {
                throw new SaffronStateException("Property grid in bad state. Did you call BeginPropertyGrid() twice?");
            }

            _begunPropertyGrid = true;

            PushID();
            ImGui.Columns(2);
            ImGui.AlignTextToFramePadding();
        }

        public static void EndPropertyGrid()
        {
            if (!_begunPropertyGrid)
            {
                throw new SaffronStateException("Property grid in bad state. Did you call EndPropertyGrid() twice?");
            }

            ImGui.Columns(1);
            PopID();

            _begunPropertyGrid = false;
        }

        public static void Property(string name, Action onClick, bool secondColumn = false)
        {
            if (secondColumn)
            {
                ImGui.NextColumn();
            }

            var id = name + "##" + name;
            if (ImGui.Button(id, new Vector2(ImGui.GetContentRegionAvail().X, 0)))
            {
                onClick();
            }

            if (!secondColumn)
            {
                ImGui.NextColumn();
            }

            ImGui.NextColumn();
        }

        public static void Property(string name, string value)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var id = "##" + name;
            ImGui.InputText(id, Encoding.ASCII.GetBytes(value), 256, ImGuiInputTextFlags.ReadOnly);

            ImGui.PopItemWidth();
            ImGui.NextColumn();
        }

        public static bool Property(string name, ref string value)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var buffer = new byte[256];
            var valueByteBuffer = Encoding.ASCII.GetBytes(value);
            Array.Copy(valueByteBuffer, buffer, Math.Min(value.Length, buffer.Length));

            var id = "##" + name;
            var changed = false;
            if (ImGui.InputText(id, buffer, (uint) buffer.Length))
            {
                value = Encoding.ASCII.GetString(buffer);
                changed = true;
            }

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref bool value)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var id = "##" + name;
            var result = ImGui.Checkbox(id, ref value);

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return result;
        }

        public static bool Property(string name, string text, string buttonName, Action onButtonPress)
        {
            ImGui.Text(name);
            ImGui.NextColumn();

            var minButtonWidth = ImGui.CalcTextSize(buttonName).X + 8.0f;
            var textBoxWidth = ImGui.GetContentRegionAvail().X - minButtonWidth;

            if (textBoxWidth > 0.0f)
            {
                ImGui.PushItemWidth(textBoxWidth);
                var stringByteBuffer = Encoding.ASCII.GetBytes(text);
                var id = "##" + name;
                ImGui.InputText(id, stringByteBuffer, (uint) stringByteBuffer.Length, ImGuiInputTextFlags.ReadOnly);
                ImGui.PopItemWidth();
                ImGui.SameLine();
            }

            var changed = false;
            var contentRegionAvailable = ImGui.GetContentRegionAvail().X;
            if (contentRegionAvailable > 0.0f)
            {
                if (ImGui.Button(buttonName, new Vector2(ImGui.GetContentRegionAvail().X, 0.0f)))
                {
                    onButtonPress?.Invoke();
                    changed = true;
                }
            }

            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref int value, int min = IntMin, int max = IntMax,
            float step = IntStep, PropertyFlag flag = PropertyFlag.None)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var oldVal = value;
            var id = "##" + name;
            var changed = flag switch
            {
                PropertyFlag.Slider => ImGui.SliderInt(id, ref value, min, max),
                PropertyFlag.Drag => ImGui.DragInt(id, ref value, step, min, max),
                PropertyFlag.ReadOnly => ImGui.DragInt(id, ref value, step, min, max),
                _ => false
            };
            
            if (changed && flag == PropertyFlag.ReadOnly)
            {
                value = oldVal;
            }

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref float value, float min = FloatMin, float max = FloatMax,
            float step = FloatStep, PropertyFlag flag = PropertyFlag.None)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var oldVal = value;
            var id = "##" + name;
            var changed = flag switch
            {
                PropertyFlag.Slider => ImGui.SliderFloat(id, ref value, min, max),
                PropertyFlag.Drag => ImGui.DragFloat(id, ref value, step, min, max),
                PropertyFlag.ReadOnly => ImGui.DragFloat(id, ref value, step, min, max),
                _ => false
            };

            if (changed && flag == PropertyFlag.ReadOnly)
            {
                value = oldVal;
            }

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref Vector2f value, PropertyFlag flag)
        {
            return Property(name, ref value, FloatMin, FloatMax, FloatStep, flag);
        }

        public static bool Property(string name, ref Vector2f value, float min = FloatMin, float max = FloatMax,
            float step = FloatStep, PropertyFlag flag = PropertyFlag.None)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var oldVal = value;
            var id = "##" + name;
            var imVec2 = ToImGuiVec2(value);
            var changed = flag switch
            {
                PropertyFlag.Slider => ImGui.SliderFloat2(id, ref imVec2, min, max),
                PropertyFlag.Drag => ImGui.DragFloat2(id, ref imVec2, step, min, max),
                PropertyFlag.ReadOnly => ImGui.DragFloat2(id, ref imVec2, step, min, max),
                _ => false
            };
            
            switch (changed)
            {
                case true when flag == PropertyFlag.ReadOnly:
                    value = oldVal;
                    break;
                case true:
                    CopyToSfmlVec2(ref value, imVec2);
                    break;
            }
            

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref Vector3f value, PropertyFlag flag)
        {
            return Property(name, ref value, FloatMin, FloatMax, FloatStep, flag);
        }

        public static bool Property(string name, ref Vector3f value, float min = FloatMin, float max = FloatMax,
            float step = FloatStep, PropertyFlag flag = PropertyFlag.None, Action fn = null)
        {
            ImGui.Text(name);
            ImGui.NextColumn();

            if (fn != null)
            {
                ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X * 0.75f);
            }
            else
            {
                ImGui.PushItemWidth(-1);
            }

            var oldVal = value;
            var id = "##" + name;
            var imVec3 = ToImGuiVec3(value);
            var changed = flag switch
            {
                PropertyFlag.Color => ImGui.ColorEdit3(id, ref imVec3, ImGuiColorEditFlags.NoInputs),
                PropertyFlag.Slider => ImGui.SliderFloat3(id, ref imVec3, min, max),
                PropertyFlag.Drag => ImGui.DragFloat3(id, ref imVec3, step, min, max),
                PropertyFlag.ReadOnly => ImGui.DragFloat3(id, ref imVec3, step, min, max),
                _ => false
            };
            
            switch (changed)
            {
                case true when flag == PropertyFlag.ReadOnly:
                    value = oldVal;
                    break;
                case true:
                    CopyToSfmlVec3(ref value, imVec3);
                    break;
            }

            if (fn != null)
            {
                var buttonId = "<" + id + "##fn";
                ImGui.SameLine();
                if (ImGui.Button(buttonId, new Vector2(ImGui.GetContentRegionAvail().X, 0.0f)))
                {
                    fn();
                    changed = false;
                }
            }

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        public static bool Property(string name, ref Vector4f value, PropertyFlag flag)
        {
            return Property(name, ref value, FloatMin, FloatMax, FloatStep, flag);
        }

        public static bool Property(string name, ref Vector4f value, float min = FloatMin, float max = FloatMax,
            float step = FloatStep, PropertyFlag flag = PropertyFlag.None)
        {
            ImGui.Text(name);
            ImGui.NextColumn();
            ImGui.PushItemWidth(-1);

            var oldVal = value;
            var id = "##" + name;
            var imVec4 = ToImGuiVec4(value);
            var changed = flag switch
            {
                PropertyFlag.Color => ImGui.ColorEdit4(id, ref imVec4, ImGuiColorEditFlags.NoInputs),
                PropertyFlag.Slider => ImGui.SliderFloat4(id, ref imVec4, min, max),
                PropertyFlag.Drag => ImGui.DragFloat4(id, ref imVec4, step, min, max),
                PropertyFlag.ReadOnly => ImGui.DragFloat4(id, ref imVec4, step, min, max),
                _ => false
            };
            
            switch (changed)
            {
                case true when flag == PropertyFlag.ReadOnly:
                    value = oldVal;
                    break;
                case true:
                    CopyToSfmlVec4(ref value, imVec4);
                    break;
            }

            ImGui.PopItemWidth();
            ImGui.NextColumn();

            return changed;
        }

        private static Vector4 ToImGuiVec4(Vector4f v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }

        private static Vector3 ToImGuiVec3(Vector3f v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        private static Vector2 ToImGuiVec2(Vector2f v)
        {
            return new Vector2(v.X, v.Y);
        }

        private static void CopyToSfmlVec4(ref Vector4f to, Vector4 from)
        {
            to.X = from.X;
            to.Y = from.Y;
            to.Z = from.Z;
            to.W = from.W;
        }

        private static void CopyToSfmlVec3(ref Vector3f to, Vector3 from)
        {
            to.X = from.X;
            to.Y = from.Y;
            to.Z = from.Z;
        }

        private static void CopyToSfmlVec2(ref Vector2f to, Vector2 from)
        {
            to.X = from.X;
            to.Y = from.Y;
        }
    }
}