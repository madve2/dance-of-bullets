using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using System.Collections;
using System.Windows.Markup;
using DoB.Xaml;

namespace DoB.Behaviors
{
    [ContentProperty("Behaviors"), Obsolete("Use BehaviorBag instead")]
    public class BehaviorSequence : Behavior, IGunConfigurator
    {
        public List<BehaviorMetadata> Behaviors { get; set; }

        public bool ResetOnLoop { get; set; }

        double creationTimeMs = -1;
        bool isStarted = false;

        public BehaviorSequence()
        {
            Behaviors = new List<BehaviorMetadata>();
        }

        public override IPrototype Clone()
        {
            var b = (BehaviorSequence)base.Clone();
            b.Behaviors = Behaviors.ToList();
            for (int i = 0; i < Behaviors.Count; i++)
            {
                b.Behaviors[i] = Behaviors[i].Clone();
            }

            b.creationTimeMs = -1;
            b.isStarted = false;
            originals = null;

            return b;
        }

        public List<BehaviorMetadata> originals { get; set; }
        public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
        {
            base.OnFirstUpdate(gameTime, gameObject);
            creationTimeMs = gameTime.TotalGameTime.TotalMilliseconds;
            originals = new List<BehaviorMetadata>();
            for (int i = 0; i < Behaviors.Count; i++)
            {
                originals.Add(Behaviors[i].Clone());
            }
        }

        public override void UpdateOverride(GameTime gameTime, GameObject gameObject) //TODO: should take GameSpeedManager into account!!
        {
            base.UpdateOverride(gameTime, gameObject);

            bool updated = false;
            for (int i = 0; i < Behaviors.Count; i++)
            {
                var actualStartTime = Behaviors[i].StartTimeMs + creationTimeMs;
                if (actualStartTime < gameTime.TotalGameTime.TotalMilliseconds
                    && (actualStartTime + Behaviors[i].LengthMs) > gameTime.TotalGameTime.TotalMilliseconds)
                {
                    isStarted = true;
                    updated = true;
                    Behaviors[i].Behavior.Update(gameTime, gameObject);
                }
            }
            if (!updated && isStarted)
            {
                isStarted = false;
                creationTimeMs = gameTime.TotalGameTime.TotalMilliseconds;
                if (ResetOnLoop)
                    ResetBehaviors();
            }
        }

        private void ResetBehaviors()
        {
            for (int i = 0; i < Behaviors.Count; i++)
            {
                Behaviors[i] = originals[i].Clone();
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
                    if (!(Behaviors[i].Behavior is IGunConfigurator))
                        continue;

                    ((IGunConfigurator)Behaviors[i].Behavior).BulletTintOverride = value;
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
                    if (!(Behaviors[i].Behavior is IGunConfigurator))
                        continue;

                    ((IGunConfigurator)Behaviors[i].Behavior).BulletTextureOverride = value;
                }
            }
        }
    }
}
