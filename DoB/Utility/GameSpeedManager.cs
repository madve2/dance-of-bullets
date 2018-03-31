using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Utility
{
	public static class GameSpeedManager
	{
		public static double GlobalModifier { get; set; } = 1;

		public static double DifficultyMultiplier { get; set; } = 1;

		public static bool IsPaused { get; set; }

		public static double ApplySpeed(double v, double ms)
		{
			return (IsPaused) ? 0 : v * (ms / 1000d) * GlobalModifier * DifficultyMultiplier;
		}

		public static void TogglePause()
		{
			IsPaused = !IsPaused;
		}

		public static void ReduceDifficulty()
		{
			if (DifficultyMultiplier > 0.8)
			{
				DifficultyMultiplier -= DifficultyMultiplier > 1 ? 0.4 : 0.2;
			}
		}

		public static void IncreaseDifficulty()
		{
			if (DifficultyMultiplier < 1.5)
			{
				DifficultyMultiplier += 0.2;
			}
		}
	}
}
