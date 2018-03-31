using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Components;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using DoB.Utility;

namespace DoB.Components
{
	[ContentProperty("Components")]
	public class Stage : IComponent
	{
		public bool IsEnded { get; set; }

		public string BackgroundTexture { get; set; }

		private string _BackgroundTextures;
		public string BackgroundTextures
		{
			get { return _BackgroundTextures; }
			set
			{
				if (_BackgroundTextures == value)
					return;
				_BackgroundTextures = value;
				BackgroundTextureArray = _BackgroundTextures.Split(';');
			}
		}

		public string[] BackgroundTextureArray { get; set; }

		private string _EndsOnEvent;
		public string EndsOnEvent
		{
			get { return _EndsOnEvent; }
			set
			{
				_EndsOnEvent = value;
				EventBroker.SubscribeOnce(value, s => IsEnded = true);
			}
		}

		public List<IComponent> Components { get; set; }

		public Stage()
		{
			Components = new List<IComponent>();
		}

		public void Update(GameTime gameTime)
		{
			for (int i = 0; i < Components.Count; i++)
			{
				Components[i].Update(gameTime);
			}
		}
	}
}
