using Time = SFML.System.Time;

namespace Saffron2D.Core
{
    public static class Global
    {
        public class Clock
        {
            private static readonly SFML.System.Clock NativeClock = new SFML.System.Clock();

            public static Time ElapsedTime => NativeClock.ElapsedTime;

            public static Time Restart()
            {
                FrameTime =  NativeClock.Restart();
                return FrameTime;
            }

            public static Time FrameTime { get; set; } 
        }
    }
}