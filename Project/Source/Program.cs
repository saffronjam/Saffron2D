using System;
using Saffron2D.Exceptions;

namespace Project
{
    internal class Program
    {
        public static void Main()
        {
            try
            {
                var application = new ProjectApplication();
                application.Start();
            }
            catch (SaffronStateException sse)
            {
                Console.WriteLine(sse.Message);
            }
        }
    }
}
