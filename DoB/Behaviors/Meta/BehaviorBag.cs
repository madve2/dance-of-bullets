using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DoB.GameObjects;
using DoB.Behaviors;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Windows.Markup;
using DoB.Xaml;

namespace DoB.Behaviors
{
    [ContentProperty("Behaviors")]
    public class BehaviorBag : Behavior, IGunConfigurator
    {
        public List<Behavior> Behaviors { get; set; }

		public bool Loop { get; set; }

        public BehaviorBag()
        {
            Behaviors = new List<Behavior>();
        }

        public override IPrototype Clone()
        {
            var c = (BehaviorBag)base.Clone();
            c.Behaviors = new List<Behavior>();
            for (int i = 0; i < Behaviors.Count; i++)
            {
                c.Behaviors.Add((Behavior)Behaviors[i].Clone());
            }
            return c;
        }

        public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
        {
            base.UpdateOverride(gameTime, gameObject);
			bool allElapsed = true;

            for (int i = 0; i < Behaviors.Count; i++)
            {
                Behaviors[i].Update(gameTime, gameObject);
				allElapsed &= Behaviors[i].IsElapsed;
            }

			if( allElapsed && Loop )
			{
				for( int i = 0; i < Behaviors.Count; i++ )
				{
					Behaviors[i].ResetTimers();
				}
			}
        }

        private Color? _BulletTintOverride;
        public Color? BulletTintOverride
        {
            get
            {
                return _BulletTintOverride;
            }
            set
            {
                _BulletTintOverride = value;

                for (int i = 0; i < Behaviors.Count; i++)
                {
                    if (!(Behaviors[i] is IGunConfigurator))
                        continue;

                    ((IGunConfigurator)Behaviors[i]).BulletTintOverride = value;
                }
            }
        }

        private string _BulletTextureOverride;
        public string BulletTextureOverride
        {
            get
            {
                return _BulletTextureOverride;
            }
            set
            {
                _BulletTextureOverride = value;

                for (int i = 0; i < Behaviors.Count; i++)
                {
                    if (!(Behaviors[i] is IGunConfigurator))
                        continue;

                    ((IGunConfigurator)Behaviors[i]).BulletTextureOverride = value;
                }
            }
        }
    }
}
