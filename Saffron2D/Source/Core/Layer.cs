using System.Runtime.InteropServices;

using Time = SFML.System.Time;

namespace Saffron2D.Core
{
    public class Layer
    {
        public Layer()
        {

        }

        public virtual void OnAttach() { }
        public virtual void OnDetach() { }
        public virtual void OnUpdate(Time dt) { }
        public virtual void OnGuiRender() { }

    }
}