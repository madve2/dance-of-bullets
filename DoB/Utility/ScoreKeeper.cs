using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoB.Utility
{
	static class ScoreKeeper
	{
		public static double Score = 0;

		public static void AddScore(double scoreValue)
		{
			Score += scoreValue * GameSpeedManager.DifficultyMultiplier;
		}
	}
}
