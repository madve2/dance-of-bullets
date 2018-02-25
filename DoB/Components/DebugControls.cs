using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DoB.Utility;
using System.Diagnostics;
using DoB.GameObjects;

namespace DoB.Components
{
	public class DebugControls : IComponent
	{
		public void Update( GameTime gameTime )
		{
			var keys = Keyboard.GetState().GetPressedKeys();
			var activeKey = keyBindings.Keys.FirstOrDefault( k => keys.Contains( k ) );
			if( activeKey != 0) 
			{
				if( !waitForRelease )
				{
					keyBindings[activeKey]( keys );
					waitForRelease = true;
				}
				return;
			}
			waitForRelease = false;
		}

		bool waitForRelease = false;

		private Dictionary<Keys, Action<Keys[]>> keyBindings = new Dictionary<Keys, Action<Keys[]>>
		{
			{ Keys.F, (keys) =>
				{
					GameSpeedManager.GlobalModifier += keys.Contains( Keys.LeftShift ) ? 0.5 : 0.1;
					Debug.WriteLine( "Game speed set to " + GameSpeedManager.GlobalModifier );
				}
			},
			{ Keys.S, (keys) =>
				{
					GameSpeedManager.GlobalModifier -= keys.Contains( Keys.LeftShift ) ? 0.5 : 0.1;
					Debug.WriteLine( "Game speed set to " + GameSpeedManager.GlobalModifier );
				}
			},
			{ Keys.D, (keys) =>
				{
					GameSpeedManager.GlobalModifier = 1;
					Debug.WriteLine( "Game speed set to " + GameSpeedManager.GlobalModifier );
				}
			},
			{ Keys.H, (keys) =>
				{
					GameObject.Game.Player.Health.Refill();
					GameObject.Game.Player.Mana.Refill();
					GameObject.Game.Player.Payback.Refill();
				}
			},
			{ Keys.A, (keys) =>
				{
					GameObject.Game.Debug_ToggleShowBulletPaths();
				}
			},
			{ Keys.N, (keys) =>
				{
					GameObject.Game.Debug_SkipStage();
				}
			},
			{ Keys.I, (keys) =>
				{
					GameObject.Game.Player.debug_IsInvincible = !GameObject.Game.Player.debug_IsInvincible;
					Debug.WriteLine( "Invincibility: " + GameObject.Game.Player.debug_IsInvincible );
				}
			},
			{ Keys.K, (keys) =>
				{
					GameObject.Game.ClearHostileObjects<Collideable>();
				}
			}
		};
	}
}
