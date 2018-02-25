using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using DoB.Xaml;

namespace DoB.Behaviors
{
    [ContentProperty("BulletPrototype")]
    public abstract class GunBehaviorBase : Behavior, IGunConfigurator
    {
        private Bullet _BulletPrototype;
        public virtual Bullet BulletPrototype
        {
            get { return _BulletPrototype; }
            set
            {
                _BulletPrototype = value;
                originalBulletTint = _BulletPrototype.Tint;
                originalBulletTexture = _BulletPrototype.BaseTexture;
                if (BulletTintOverride != null)
                    _BulletPrototype.Tint = BulletTintOverride.Value;
            }
        }

        public override IPrototype Clone()
        {
            var c = (GunBehaviorBase)base.Clone();
            c.BulletPrototype = (Bullet)BulletPrototype.Clone();
            return c;
        }

        private Color originalBulletTint;
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
                if (BulletPrototype == null)
                    return;

                if (value != null)
                    BulletPrototype.Tint = value.Value;
                else
                    BulletPrototype.Tint = originalBulletTint;
            }
        }

        private string originalBulletTexture;
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
                if (BulletPrototype == null)
                    return;

                if (value != null)
                    BulletPrototype.BaseTexture = value;
                else
                    BulletPrototype.BaseTexture = originalBulletTexture;
            }
        }
    }
}
