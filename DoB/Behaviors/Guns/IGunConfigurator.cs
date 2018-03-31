using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using Microsoft.Xna.Framework;

namespace DoB.Behaviors
{
	public interface IGunConfigurator
	{
		Color? BulletTintOverride { get; set; }

		string BulletTextureOverride { get; set; }
	}
}
