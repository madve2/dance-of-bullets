using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Utility
{
    public static class GameSpeedManager
    {
        public static double GlobalModifier { get; set; }

        public static bool IsPaused { get; set; }

        static GameSpeedManager()
        {
            GlobalModifier = 1;
        }

        public static double ApplySpeed(double v, double ms)
        {
            return (IsPaused) ? 0 : v * (ms / 1000d) * GlobalModifier;
        }

        internal static void TogglePause()
        {
            IsPaused = !IsPaused;
        }
    }
}
