using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;

namespace DoB.Utility
{
	public class Consumable
	{
		public double Amount {get; set;}
		public double OriginalAmount { get; private set; }

		public Consumable(double lives)
		{
			Amount = lives;
			OriginalAmount = lives;
		}

		public void Refill()
		{
			Amount = OriginalAmount;
		}

		public bool HasRunOut
		{
			get
			{
				return Amount <= 0;
			}
		}
	}
}
