using System.Collections.Generic;
using Saffron2D.Core;
using Saffron2D.Exceptions;
using SFML.Graphics;
using SFML.System;

namespace Saffron2D.Graphics
{
    public class Scene
    {
        private class DrawCommand
        {
            public DrawCommand(Drawable drawable, RenderStates renderStates, int zIndex = -1)
            {
                Drawable = drawable;
                RenderStates = renderStates;
                ZIndex = zIndex;
            }

            public Drawable Drawable { get; }

            public RenderStates RenderStates { get; }

            public int ZIndex { get; }
        }

        private readonly List<DrawCommand> _drawCommands = new List<DrawCommand>();
        public RenderTarget Target { get; set; }
        private bool _begun = false;
        private Vector2f _viewportSize;

        public RenderStates ScreenSpace { get; } = default;

        public Scene(RenderTarget target, Camera camera)
        {
            Target = target;
            Camera = camera;
        }

        public void OnUpdate()
        {
            Camera.OnUpdate();
        }

        public void Begin()
        {
            if (_begun)
            {
                throw new SaffronStateException("Scene has already begun, did you call BeginScene twice?");
            }

            _begun = true;
            _drawCommands.Clear();
        }

        public void End()
        {
            _drawCommands.Sort((elem1, elem2) => elem1.ZIndex - elem2.ZIndex);
            foreach (var drawCommand in _drawCommands)
            {
                Target.Draw(drawCommand.Drawable, drawCommand.RenderStates);
            }

            _begun = false;
        }

        public void Submit(Drawable drawable, int zIndex = -1)
        {
            Submit(drawable, RenderStates.Default, zIndex);
        }

        public void Submit(Drawable drawable, RenderStates renderStates, int zIndex = -1)
        {
            renderStates.Transform.Combine(Camera.Transform);
            _drawCommands.Add(new DrawCommand(drawable, renderStates, zIndex));
        }

        public void SubmitScreenSpace(Drawable drawable, RenderStates renderStates, int zIndex = -1)
        {
            _drawCommands.Add(new DrawCommand(drawable, renderStates, zIndex));
        }

        public Camera Camera { get; set; }

        public Vector2f ViewportSize
        {
            get => _viewportSize;
            set
            {
                _viewportSize = value;
                Camera.ViewportSize = _viewportSize;
            }
        }
    }
}