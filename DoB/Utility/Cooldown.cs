using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DoB.Utility
{
	public class Cooldown
	{
		double cooldown;
		double totalCooldown;

		public Cooldown()
		{
		}

		public Cooldown(double ms) : base()
		{
			totalCooldown = ms;
			cooldown = ms;
		}

		public void Reset(double ms)
		{
			totalCooldown = ms;
			cooldown = ms;
		}

		public void Update(double elapsedTimeMs)
		{
			if (!IsElapsed)
				cooldown -= GameSpeedManager.ApplySpeed(1000, elapsedTimeMs);
		}

		public bool IsElapsed
		{
			get
			{
				return cooldown <= 0;
			}
		}

		public float Progress
		{
			get
			{
				return MathHelper.Clamp( 1 - (float)(cooldown / totalCooldown), 0, 1 );
			}
		}
	}
}
