using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DoB.Components
{
	public interface IComponent
	{
		void Update(GameTime gameTime);
	}
}
