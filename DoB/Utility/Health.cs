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

        public Health(GameObject g, double lives) : base(lives)
        {
            this.gameObject = g;
        }

        public bool Hit()
        {
            if (--this.Amount == 0)
            {
                gameObject.RemoveSelf();
                return true;
            }
            return false;
        }
    }
}
