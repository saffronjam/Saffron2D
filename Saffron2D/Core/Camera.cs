using System;
using System.Runtime.InteropServices;
using System.Security;
using ImGuiNET;
using Saffron2D.GuiCollection;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Saffron2D.Core
{
    public class Camera
    {
        private Transform _transform;
        private Transform _rotationTransform;
        private Transform _zoomTransform;

        private Vector2f _position;
        private float _rotation;
        private Vector2f _zoom;

        private Vector2f? _follow;

        public Camera(float rotationSpeed = 1.0f)
        {
            RotationSpeed = rotationSpeed;
            Center = new Vector2f(0, 0);
            Zoom = new Vector2f(1, 1);
            Rotation = 0.0f;
            ViewportSize = new Vector2f(10, 10);

            ActiveController = true;
        }

        public void OnUpdate()
        {
            if (!ActiveController)
            {
                return;
            }

            var dt = Global.Clock.FrameTime;

            if (_follow.HasValue)
            {
                Center = _follow.Value;
            }
            else
            {
                if (Input.IsKeyDown(Keyboard.Key.LControl) &&
                    Input.IsMouseButtonDown(Mouse.Button.Right) &&
                    !Input.IsMouseButtonDown(Mouse.Button.Left))
                {
                    var delta = Input.MouseSwipe;
                    if (Utils.VecUtils.LengthSq(delta) > 0.0f)
                    {
                        delta.Y *= -1.0f;
                        delta = _rotationTransform.GetInverse().TransformPoint(delta);
                        delta = _zoomTransform.GetInverse().TransformPoint(delta);
                        delta *= -1.0f;
                        ApplyMovement(delta);
                    }
                }
            }

            ApplyZoom((Input.VerticalScroll / 100.0f) + 1.0f);

            var angle = 0.0f;
            var angleDiff = RotationSpeed * 360.0f * dt.AsSeconds();

            if (Input.IsKeyDown(Keyboard.Key.Q))
            {
                angle += angleDiff;
            }

            if (Input.IsKeyDown(Keyboard.Key.E))
            {
                angle -= angleDiff;
            }

            ApplyRotation(angle);

            if (Input.IsKeyPressed(Keyboard.Key.R))
            {
                ResetTransformation();
            }
        }

        public void OnGuiRender()
        {
            if (ImGui.Begin("Camera"))
            {
                Gui.BeginPropertyGrid();
                Gui.Property("Position", ref _position, -10000.0f, 10000.0f, 10.0f, Gui.PropertyFlag.Drag);
                if (Gui.Property("Zoom", ref _zoom.X, 0.01f, 10.0f, 0.01f, Gui.PropertyFlag.Slider))
                {
                    _zoom.Y = _zoom.X;
                }
                Gui.Property("Rotation", ref _rotation, -180.0f, 180.0f, 1.0f, Gui.PropertyFlag.Slider);
                var rotationSpeed = RotationSpeed;
                if (Gui.Property("Rotation Speed", ref rotationSpeed, 0.0f, 10.0f, 0.1f, Gui.PropertyFlag.Slider))
                {
                    RotationSpeed = rotationSpeed;
                }

                if (!_follow.HasValue)
                {
                    Gui.Property("Follow", "Not set");
                }
                else
                {
                    var vector2F = _follow.Value;
                    Gui.Property("Follow", ref vector2F, Gui.PropertyFlag.ReadOnly);
                }

                Gui.EndPropertyGrid();

                ImGui.End();
            }
        }

        public bool ActiveController { get; set; }

        public Transform Transform
        {
            get => _transform;
            private set => _transform = value;
        }

        public void ApplyMovement(Vector2f offset)
        {
            Center += offset;
        }

        public void ApplyZoom(float factor)
        {
            Zoom *= factor;
        }

        public void ApplyRotation(float angle)
        {
            Rotation += angle;
        }

        public Vector2f Center
        {
            get => _position;
            set
            {
                _position = value;
                UpdateTransform();
            }
        }

        public Vector2f Zoom
        {
            get => _zoom;
            set
            {
                if (value.X == 0.0f || value.Y == 0.0f)
                {
                    return;
                }

                _zoom = value;
                _zoomTransform = Transform.Identity;
                _zoomTransform.Scale(_zoom);
                UpdateTransform();
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                if (_rotation < -180.0f)
                {
                    _rotation = 180.0f;
                }
                if (_rotation > 180.0f)
                {
                    _rotation = -180.0f;
                }
                
                _rotationTransform = Transform.Identity;
                _rotationTransform.Rotate(_rotation);
                UpdateTransform();
            }
        }

        public float RotationSpeed { get; set; }

        public Vector2f Follow { get; set; }

        public void Unfollow()
        {
            _follow = null;
        }

        public Vector2f ViewportSize { get; set; }

        ///Translate a point to world space
        ///@param point: point to be translated from screen to world space.
        public Vector2f ScreenToWorld(Vector2f point)
        {
            return Transform.GetInverse().TransformPoint(point);
        }

        ///Translate a rect to world space
        ///@param rect: rect to be translated from screen to world space.
        public FloatRect ScreenToWorld(FloatRect rect)
        {
            return Transform.GetInverse().TransformRect(rect);
        }

        ///Translate a point to world space
        ///@param rect: rect to be translated from world to screen space.
        public Vector2f WorldToScreen(Vector2f point)
        {
            return Transform.TransformPoint(point);
        }

        ///Translate a rect to world space
        ///@param rect: rect to be translated from world to screen space.
        public FloatRect WorldToScreen(FloatRect rect)
        {
            return Transform.TransformRect(rect);
        }

        private void UpdateTransform()
        {
            _transform = Transform.Identity;
            _transform.Translate(ViewportSize / 2.0f);
            _transform.Scale(Zoom);
            _transform.Rotate(Rotation);
            _transform.Translate(-Center);
        }

        private void CapZoomLevel(float lower = 0.9f, float upper = 3.0f)
        {
            var current = Zoom;

            if (Zoom.X < lower)
            {
                current.X = lower;
            }
            else if (Zoom.X > upper)
            {
                current.X = upper;
            }

            if (Zoom.Y < lower)
            {
                current.Y = lower;
            }
            else if (Zoom.Y > upper)
            {
                current.Y = upper;
            }

            Zoom = current;
        }

        private void ResetTransformation()
        {
            Center = new Vector2f(0.0f, 0.0f);
            Rotation = 0.0f;
            Zoom = new Vector2f(1.0f, 1.0f);

            UpdateTransform();
        }
    }
}