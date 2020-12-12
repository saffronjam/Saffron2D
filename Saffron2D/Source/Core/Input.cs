using System;
using System.Collections;
using SFML.Window;

using Key = SFML.Window.Keyboard.Key;
using MouseButton = SFML.Window.Mouse.Button;

namespace Saffron2D.Core
{
    public class Input
    {
        private static readonly BitArray keyboardState = new BitArray((int)Key.KeyCount);
        private static readonly BitArray lastKeyboardState = new BitArray((int)Key.KeyCount);
        private static readonly BitArray mouseState = new BitArray((int)MouseButton.ButtonCount);
        private static readonly BitArray lastMouseState = new BitArray((int)MouseButton.ButtonCount);

        public static void AddEventSource(Window eventSource)
        {
            var nativeWindow = eventSource.NativeWindow;
            nativeWindow.KeyPressed += OnKeyPress;
            nativeWindow.KeyReleased += OnKeyRelease;
            nativeWindow.MouseButtonPressed += OnMouseButtonPress;
            nativeWindow.MouseButtonReleased += OnMouseButtonRelease;
        }
        public static void RemoveEventSource(Window eventSource)
        {
            var nativeWindow = eventSource.NativeWindow;
            nativeWindow.KeyPressed -= OnKeyPress;
            nativeWindow.KeyReleased -= OnKeyRelease;
            nativeWindow.MouseButtonPressed -= OnMouseButtonPress;
            nativeWindow.MouseButtonReleased -= OnMouseButtonRelease;
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
        }

        public static bool IsKeyDown(Key key)
        {
            return keyboardState[(int)key];
        }

        public static bool IsKeyUp(Key key)
        {
            return !keyboardState[(int)key];
        }

        public static bool IsKeyPressed(Key key)
        {
            return keyboardState[(int)key] && !lastKeyboardState[(int)key];
        }
        public static bool IsKeyReleased(Key key)
        {
            return !keyboardState[(int)key] && lastKeyboardState[(int)key];
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return mouseState[(int)button];
        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            return !mouseState[(int)button];
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return mouseState[(int)button] && !lastMouseState[(int)button];
        }
        public static bool IsMouseButtonReleased(MouseButton button)
        {
            return !mouseState[(int)button] && lastMouseState[(int)button];
        }

        private static void OnKeyPress(object sender, KeyEventArgs args)
        {
            keyboardState[(int)args.Code] = true;
        }

        private static void OnKeyRelease(object sender, KeyEventArgs args)
        {
            keyboardState[(int)args.Code] = false;
        }

        private static void OnMouseButtonPress(object sender, MouseButtonEventArgs args)
        {
            mouseState[(int)args.Button] = true;
        }
        private static void OnMouseButtonRelease(object sender, MouseButtonEventArgs args)
        {
            mouseState[(int)args.Button] = false;
        }
    }
}