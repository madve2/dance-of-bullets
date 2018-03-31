using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DoB.GameObjects;
using DoB.Drawers;
using DoB.Behaviors;
using DoB.Utility;
using System.IO;
using System.Xaml;
using DoB.Xaml;
using System.Diagnostics;
using DoB.Components;
using System.Globalization;
using DoB.Extensions;

namespace DoB
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class DoBGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		RenderTarget2D renderTarget;
		RenderTarget2D renderTargetTmp;

		public List<GameObject> Objects = new List<GameObject>();
		public List<IComponent> ShmupComponents = new List<IComponent>();
		public Player Player = null;

		public Rectangle GameplayRectangle { get; set; }
		public Rectangle DrawingRectangle { get; set; }

		public int StageIndex { get; set; }
		public Stages Stages { get; set; }
		public Stage CurrentStage
		{
			get
			{
				return ( StageIndex >= 0 && StageIndex < Stages.Count ) ? Stages[StageIndex] : null;
			}
		}

		private int debug_collChecksPeak = 0;
		private bool debug_showBulletPaths = false;

		private Cooldown nextStageDelayCooldown = new Cooldown();
		private bool isStageEnding = false;

		private Cooldown stageTransitionEffectCooldown = new Cooldown();
		private Texture2D stageTransitionEffect;
		private Config cfg;

		public DoBGame()
		{
			graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			cfg = (Config)XamlServices.Parse( File.ReadAllText( "Config.xaml" ) );
			GameplayRectangle = new Rectangle( 0, 0, 1280, 720 ); //the size of the gameplay area, regardless of the actually rendered resolution
			DrawingRectangle = new Rectangle( 0, 0, (int)cfg["ResolutionW"], (int)cfg["ResolutionH"] );
			graphics.PreferredBackBufferWidth = DrawingRectangle.Width;
			graphics.PreferredBackBufferHeight = DrawingRectangle.Height;
            graphics.IsFullScreen = (bool)cfg["IsFullScreen"];
			graphics.ApplyChanges();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			GameObject.Game = this;
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch( GraphicsDevice );

			InitializeRenderTarget();
			renderTargetTmp = new RenderTarget2D( GraphicsDevice, DrawingRectangle.Width, DrawingRectangle.Height );

			font = Content.Load<SpriteFont>( "SpriteFont1" );
			stageTransitionEffect = Content.Load<Texture2D>( "white" );

			Stages = ( (Stages)XamlServices.Parse( File.ReadAllText( (string)cfg["StageDataFile"] ) ) );
			StageIndex = 0;
			this.ShmupComponents.Add( Stages[StageIndex] );

            LoadStageTextures();

			Player = new Player { X = 300, Y = 360 };
			Objects.Add( Player );
		}

        private void LoadStageTextures()
        {
            if (Stages[StageIndex].BackgroundTextures != null)
            {
                bgTextures = new List<Texture2D>();
                bgX = new List<double>();
                for (int i = 0; i < Stages[StageIndex].BackgroundTextureArray.Length; i++)
                {
                    bgTextures.Add(Content.Load<Texture2D>(Stages[StageIndex].BackgroundTextureArray[i]));
                    bgX.Add(0);
                }
            }
            else
            {
                bgTextures = new List<Texture2D> { Content.Load<Texture2D>(Stages[StageIndex].BackgroundTexture) };
                bgX = new List<double> { 0 };
            }
        }

		private void InitializeRenderTarget()
		{
			if( !debug_showBulletPaths )
			{
				renderTarget = new RenderTarget2D( GraphicsDevice, GameplayRectangle.Width, GameplayRectangle.Height );
			}
			else
			{
				renderTarget = new RenderTarget2D( GraphicsDevice, GameplayRectangle.Width, GameplayRectangle.Height,
					false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents );
			}
		}

		internal void Debug_ToggleShowBulletPaths()
		{
			debug_showBulletPaths = !debug_showBulletPaths;
			InitializeRenderTarget();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>

		protected override void Update( GameTime gameTime )
		{
			// Allows the game to exit
			if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) )
				this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }
                

			Objects.RemoveAll( c => c.IsMarkedForRemoval );

			for( int i = 0; i < ShmupComponents.Count; i++ )
			{
				ShmupComponents[i].Update( gameTime );
			}

			for( int i = 0; i < Objects.Count; i++ )
			{
				Objects[i].Update( gameTime );
			}

			var debug_collChecks = 0;
			for( int i = 0; i < Objects.Count; i++ )
			{
				if( !( Objects[i] is Collideable ) || !( (Collideable)Objects[i] ).IsFriendly )
					continue;

				var ao = (Collideable)Objects[i];
				for( int j = 0; j < Objects.Count; j++ )
				{
					if( !( Objects[j] is Collideable ) || ( (Collideable)Objects[j] ).IsFriendly )
						continue;

					var bo = (Collideable)Objects[j];
					debug_collChecks++;
					if( Vector2.Distance( new Vector2( (float)ao.X, (float)ao.Y ), new Vector2( (float)bo.X, (float)bo.Y ) ) < ( (float)ao.CollisionRadius + (float)bo.CollisionRadius ) )
					{
						ao.CollideWith( bo );
						bo.CollideWith( ao );
					}
				}
			}

			debug_collChecksPeak = Math.Max( debug_collChecks, debug_collChecksPeak );

			if( !isStageEnding && Stages[StageIndex].IsEnded )
			{
				EndStage();
			}

			nextStageDelayCooldown.Update( gameTime.ElapsedMs() );
			if( isStageEnding && nextStageDelayCooldown.IsElapsed )
			{
				NextStage();
			}

			stageTransitionEffectCooldown.Update( gameTime.ElapsedMs() );

			base.Update( gameTime );
		}

		private void EndStage()
		{
			EventBroker.IsEnabled = false;
			ShmupComponents.Remove( Stages[StageIndex] );
			ClearHostileObjects<Collideable>();
			isStageEnding = true;
			nextStageDelayCooldown.Reset( 2000 );
		}

		private void NextStage()
		{
			if( ++StageIndex < Stages.Count )
			{
				EventBroker.IsEnabled = true;
				ShmupComponents.Add( Stages[StageIndex] );
                LoadStageTextures();
				stageTransitionEffectCooldown.Reset( 5000 );
				isStageEnding = false;
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime )
		{
			DrawGameToTexture( gameTime );

			spriteBatch.Begin();
			spriteBatch.Draw( renderTarget, DrawingRectangle, Color.White );
			spriteBatch.End();

			base.Draw( gameTime );
		}

		private void DrawGameToTexture( GameTime gameTime )
		{
			GraphicsDevice.SetRenderTarget( renderTarget );
			spriteBatch.Begin();

            if (!debug_showBulletPaths)
            {
                for (int i = 0; i < bgTextures.Count; i++)
                {
                    var x = bgX[i];
                    DrawBackground(gameTime, bgTextures[i], -600 - i * 150, 1601, ref x);
                    bgX[i] = x;
                }
            }

			for( int i = 0; i < Objects.Count; i++ )
			{
				Objects[i].Draw( gameTime, spriteBatch );
			}

            spriteBatch.DrawString( font, $"Health: {Player.Health.Amount} | Multiplier: {GameSpeedManager.DifficultyMultiplier} | Score: {ScoreKeeper.Score}", new Vector2( 650, 6 ), Color.White );

            if ( !stageTransitionEffectCooldown.IsElapsed )
			{
				spriteBatch.Draw( stageTransitionEffect, GameplayRectangle, Color.FromNonPremultiplied( 255, 255, 255, (int)( 255 * ( 1 - stageTransitionEffectCooldown.Progress ) ) ) );
			}

			spriteBatch.End();
			GraphicsDevice.SetRenderTarget( null );
		}

		List<Texture2D> bgTextures = null;
        List<double> bgX = null;

		private void DrawBackground( GameTime gameTime, Texture2D bgTexture, double v, int textureWidth, ref double x, int y = 0 )
		{
			x += (double)GameSpeedManager.ApplySpeed( v, gameTime.ElapsedGameTime.TotalMilliseconds );
			if( x < -textureWidth )
				x = 0;
			var color = Player.IsPaybackActive ? Color.FromNonPremultiplied( 255, 50, 255, 255 )
				: Player.IsManaActive ? Color.FromNonPremultiplied( 50, 50, 255, 255 ) : Color.White;
			spriteBatch.Draw( bgTexture, new Rectangle( (int)x, y, textureWidth, 768 ), color );
			spriteBatch.Draw( bgTexture, new Rectangle( (int)( x < 0 ? x + textureWidth : x - textureWidth ), y, textureWidth, 768 ), color );
		}

		DateTime debug_lastSkipTime = DateTime.MinValue;
		internal void Debug_SkipStage()
		{
			if( ( DateTime.Now - debug_lastSkipTime ).TotalSeconds > 0.5 )
			{
				debug_lastSkipTime = DateTime.Now;
				EndStage();
			}
		}

		public void ClearBullets()
		{
			ClearHostileObjects<Bullet>();
		}


		internal void ClearHostileObjects<T>() where T:Collideable //It's internal so that debugcontrols can use it
		{
			foreach( T obj in Objects.Where( o => o is T && !( o as T ).IsFriendly ).ToList() )
			{
				obj.RemoveSelf();
			}
		}
	}
}
