using Time = SFML.System.Time;

namespace Saffron2D.Core
{
    public static class Global
    {
        public class Clock
        {
            private static readonly SFML.System.Clock nativeClock = new SFML.System.Clock();

            public static Time ElapsedTime => nativeClock.ElapsedTime;
            public static Time Restart() => nativeClock.Restart();
        }
    }
}