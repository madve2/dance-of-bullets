using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Xaml;

namespace DoB.Behaviors
{
	public class BehaviorReference : Behavior //You can also use PrototypeExtension; however, this one is more readable in lists, and easier to inherit from (eg. GunBehaviorReference)
	{
		protected Behavior referencedBehavior;

		private string _ReferenceName;
		public string ReferenceName
		{
			get { return _ReferenceName; }
			set
			{
				if (_ReferenceName == value)
					return;

				_ReferenceName = value;
				referencedBehavior = null;
			}
		}

		public override IPrototype Clone()
		{
			var c = (BehaviorReference)base.Clone();
			c.referencedBehavior = null;
			return c;
		}

		public override void OnFirstUpdate(Microsoft.Xna.Framework.GameTime gameTime, GameObjects.GameObject gameObject)
		{
			base.OnFirstUpdate(gameTime, gameObject);
			if (referencedBehavior == null)
				referencedBehavior = (Behavior)Prototypes.All[ReferenceName].Clone();
		}

		public override void UpdateOverride(Microsoft.Xna.Framework.GameTime gameTime, GameObjects.GameObject gameObject)
		{
			base.UpdateOverride(gameTime, gameObject);

			referencedBehavior.Update(gameTime, gameObject);
		}

	}
}
