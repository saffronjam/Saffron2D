using System;
using System.Collections;
using SFML.System;
using SFML.Window;
using Key = SFML.Window.Keyboard.Key;
using MouseButton = SFML.Window.Mouse.Button;

namespace Saffron2D.Core
{
    public class Input
    {
        private static readonly BitArray keyboardState = new BitArray((int) Key.KeyCount);
        private static readonly BitArray lastKeyboardState = new BitArray((int) Key.KeyCount);
        private static readonly BitArray mouseState = new BitArray((int) MouseButton.ButtonCount);
        private static readonly BitArray lastMouseState = new BitArray((int) MouseButton.ButtonCount);
        private static Vector2f mousePosition = new Vector2f(0, 0);
        private static Vector2f lastMousePosition = new Vector2f(0, 0);

        public static void AddEventSource(Window eventSource)
        {
            var nativeWindow = eventSource.NativeWindow;
            nativeWindow.KeyPressed += OnKeyPressed;
            nativeWindow.KeyReleased += OnKeyReleased;
            nativeWindow.MouseButtonPressed += OnMouseButtonPressed;
            nativeWindow.MouseButtonReleased += OnMouseButtonReleased;
            nativeWindow.MouseMoved += OnMouseMoved;
            nativeWindow.MouseWheelScrolled += OnMouseWheelScrolled;
        }

        public static void RemoveEventSource(Window eventSource)
        {
            var nativeWindow = eventSource.NativeWindow;
            nativeWindow.KeyPressed -= OnKeyPressed;
            nativeWindow.KeyReleased -= OnKeyReleased;
            nativeWindow.MouseButtonPressed -= OnMouseButtonPressed;
            nativeWindow.MouseButtonReleased -= OnMouseButtonReleased;
            nativeWindow.MouseWheelScrolled -= OnMouseWheelScrolled;
        }

        public static void OnUpdate()
        {
            for (var i = 0; i < keyboardState.Count; i++)
            {
                lastKeyboardState[i] = keyboardState[i];
            }

            for (var i = 0; i < mouseState.Count; i++)
            {
                lastMouseState[i] = mouseState[i];
            }

            lastMousePosition = mousePosition;
            VerticalScroll = 0.0f;
            HorizontalScroll = 0.0f;
        }

        public static bool IsKeyDown(Key key)
        {
            return keyboardState[(int) key];
        }

        public static bool IsKeyUp(Key key)
        {
            return !keyboardState[(int) key];
        }

        public static bool IsKeyPressed(Key key)
        {
            return keyboardState[(int) key] && !lastKeyboardState[(int) key];
        }

        public static bool IsKeyReleased(Key key)
        {
            return !keyboardState[(int) key] && lastKeyboardState[(int) key];
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return mouseState[(int) button];
        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            return !mouseState[(int) button];
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return mouseState[(int) button] && !lastMouseState[(int) button];
        }

        public static bool IsMouseButtonReleased(MouseButton button)
        {
            return !mouseState[(int) button] && lastMouseState[(int) button];
        }

        public static Vector2f MousePosition
        {
            get => mousePosition;
            private set => mousePosition = value;
        }

        public static Vector2f MouseSwipe => MousePosition - lastMousePosition;

        public static float VerticalScroll { get; set; }
        public static float HorizontalScroll { get; set; }


        // Event handlers

        private static void OnKeyPressed(object sender, KeyEventArgs args)
        {
            keyboardState[(int) args.Code] = true;
        }

        private static void OnKeyReleased(object sender, KeyEventArgs args)
        {
            keyboardState[(int) args.Code] = false;
        }

        private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs args)
        {
            mouseState[(int) args.Button] = true;
        }

        private static void OnMouseButtonReleased(object sender, MouseButtonEventArgs args)
        {
            mouseState[(int) args.Button] = false;
        }

        private static void OnMouseMoved(object sender, MouseMoveEventArgs args)
        {
            lastMousePosition = mousePosition;
            mousePosition.X = args.X;
            mousePosition.Y = args.Y;
        }

        private static void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs args)
        {
            switch (args.Wheel)
            {
                case Mouse.Wheel.HorizontalWheel:
                    HorizontalScroll = args.Delta;
                    break;
                case Mouse.Wheel.VerticalWheel:
                    VerticalScroll = args.Delta;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            mousePosition.X = args.X;
            mousePosition.Y = args.Y;
        }
    }
}