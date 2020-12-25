using System.Collections.Generic;
using System.Linq;
using Saffron2D.Core;
using SFML.Graphics;

namespace Saffron2D.Graphics
{
    public static class RenderTargetManager
    {
        private static readonly List<ControllableRenderTarget> Targets = new List<ControllableRenderTarget>();

        public static void Add(ControllableRenderTarget renderTarget)
        {
            Targets.Add(renderTarget);
        }

        public static void ClearAll()
        {
            foreach (var target in Targets.Where(target => target.Enabled))
            {
                target.RenderTarget.Clear(target.ClearColor);
            }
        }

        public static void DisplayAll()
        {
            foreach (var target in Targets.Where(target => target.Enabled))
            {
                switch (target.RenderTarget)
                {
                    case RenderWindow window:
                        window.Display();
                        break;
                    case RenderTexture texture:
                        texture.Display();
                        break;
                }
            }
        }
    }
}