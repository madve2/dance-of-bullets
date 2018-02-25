using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Behaviors;
using DoB.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DoB.Utility;
using DoB.Xaml;

namespace DoB.Behaviors
{
    public class PlayerControlBehavior : Behavior
    {
        private bool lockMana = false;
        private bool lockPause = false;
        private double v = 300;
        private Cooldown gunCooldown = new Cooldown();
        public override IPrototype Clone()
        {
            var c = (PlayerControlBehavior)base.Clone();
            c.gunCooldown = new Cooldown();
            return c;
        }

        public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
        {
            base.OnFirstUpdate(gameTime, gameObject);
        }

        public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
        {
            base.UpdateOverride(gameTime, gameObject);
            var keys = Keyboard.GetState().GetPressedKeys();
            gunCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            if (keys.Contains(Keys.C) || keys.Contains(Keys.Space))
            {
                if (gunCooldown.IsElapsed)
                {
					( (Player)gameObject ).Shoot();
                    gunCooldown.Reset( ( (Player)gameObject ).IsPaybackActive ? 10 : 120);
                }
            }
            if (keys.Contains(Keys.Up))
            {
                gameObject.MoveY -= GameSpeedManager.ApplySpeed(v, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (keys.Contains(Keys.Down))
            {
                gameObject.MoveY += GameSpeedManager.ApplySpeed(v, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (keys.Contains(Keys.Left))
            {
                gameObject.MoveX -= GameSpeedManager.ApplySpeed(v, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (keys.Contains(Keys.Right))
            {
                gameObject.MoveX += GameSpeedManager.ApplySpeed(v, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (keys.Contains(Keys.X))
            {
                if (lockMana)
                    return;
                if (((Player)gameObject).IsManaActive)
                    ((Player)gameObject).StopMana();
                else
                    ((Player)gameObject).StartMana();

                lockMana = true;
            }
            else
            {
                lockMana = false;
            }
			if( keys.Contains( Keys.Y ))
			{
				( (Player)gameObject ).ActivatePayback();
			}
            if (keys.Contains(Keys.P) || keys.Contains(Keys.B))
            {
                if (lockPause)
                    return;

                GameSpeedManager.TogglePause();

                lockPause = true;
            }
            else
            {
                lockPause = false;
            }

        }
    }
}

