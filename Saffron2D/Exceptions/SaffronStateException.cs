using System;
using SFML.Window;


namespace Saffron2D.Exceptions
{
    public class SaffronStateException : InvalidOperationException
    {
        public SaffronStateException(string message) : base(message)
        {
        }
    }
}