using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DoB.Extensions
{
	public static class GameTimeExtensions
	{
		public static double TotalMs( this GameTime THIS )
		{
			return THIS.TotalGameTime.TotalMilliseconds;
		}

		public static double ElapsedMs( this GameTime THIS )
		{
			return THIS.ElapsedGameTime.TotalMilliseconds;
		}
	}
}
