using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using DoB.Behaviors;
using Microsoft.Xna.Framework;

namespace DoB.Utility
{
	public class Health : Consumable
	{
		private GameObject gameObject;
		private double ScoreValue = 0;

		public Health(GameObject g, double lives) : base(lives)
		{
			this.gameObject = g;
			if (!(g is Player))
			{
				ScoreValue = lives * 100;
			}
		}

		public bool Hit()
		{
			if (--this.Amount == 0)
			{
				gameObject.RemoveSelf();
				ScoreKeeper.AddScore(ScoreValue);
				return true;
			}
			return false;
		}
	}
}
