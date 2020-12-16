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
        private static readonly BitArray KeyboardState = new BitArray((int) Key.KeyCount);
        private static readonly BitArray LastKeyboardState = new BitArray((int) Key.KeyCount);
        private static readonly BitArray MouseState = new BitArray((int) MouseButton.ButtonCount);
        private static readonly BitArray LastMouseState = new BitArray((int) MouseButton.ButtonCount);
        private static Vector2f _mousePosition = new Vector2f(0, 0);
        private static Vector2f _lastMousePosition = new Vector2f(0, 0);

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
            for (var i = 0; i < KeyboardState.Count; i++)
            {
                LastKeyboardState[i] = KeyboardState[i];
            }

            for (var i = 0; i < MouseState.Count; i++)
            {
                LastMouseState[i] = MouseState[i];
            }

            _lastMousePosition = _mousePosition;
            VerticalScroll = 0.0f;
            HorizontalScroll = 0.0f;
        }

        public static bool IsKeyDown(Key key)
        {
            return KeyboardState[(int) key];
        }

        public static bool IsKeyUp(Key key)
        {
            return !KeyboardState[(int) key];
        }

        public static bool IsKeyPressed(Key key)
        {
            return KeyboardState[(int) key] && !LastKeyboardState[(int) key];
        }

        public static bool IsKeyReleased(Key key)
        {
            return !KeyboardState[(int) key] && LastKeyboardState[(int) key];
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return MouseState[(int) button];
        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            return !MouseState[(int) button];
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return MouseState[(int) button] && !LastMouseState[(int) button];
        }

        public static bool IsMouseButtonReleased(MouseButton button)
        {
            return !MouseState[(int) button] && LastMouseState[(int) button];
        }

        public static Vector2f MousePosition
        {
            get => _mousePosition;
            private set => _mousePosition = value;
        }

        public static Vector2f MouseSwipe => MousePosition - _lastMousePosition;

        public static float VerticalScroll { get; set; }
        public static float HorizontalScroll { get; set; }


        // Event handlers

        private static void OnKeyPressed(object sender, KeyEventArgs args)
        {
            KeyboardState[(int) args.Code] = true;
        }

        private static void OnKeyReleased(object sender, KeyEventArgs args)
        {
            KeyboardState[(int) args.Code] = false;
        }

        private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs args)
        {
            MouseState[(int) args.Button] = true;
        }

        private static void OnMouseButtonReleased(object sender, MouseButtonEventArgs args)
        {
            MouseState[(int) args.Button] = false;
        }

        private static void OnMouseMoved(object sender, MouseMoveEventArgs args)
        {
            _lastMousePosition = _mousePosition;
            _mousePosition.X = args.X;
            _mousePosition.Y = args.Y;
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
            _mousePosition.X = args.X;
            _mousePosition.Y = args.Y;
        }
    }
}