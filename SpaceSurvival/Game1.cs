#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace SpaceSurvival
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{

		public const int NUM_FRAMES = 6;
		public const int WINDOW_WIDTH = 640;
		public const int WINDOW_HEIGHT = 475;
		string[] frameName = new string[NUM_FRAMES];

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D background;
		Rectangle mainFrame, newFrame;
		Player X10;
		KeyboardState keyboard;
		MouseState mouse;
		ButtonState previousButtonState = ButtonState.Released;
		EnemyManager enemyManager;
		BulletManager bulletManager;
		ExplosionManager explosionManager;
		SpriteFont HighscoreSprite;
		public Game1()
		{

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			graphics.IsFullScreen = false;

		}
		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{

			spriteBatch = new SpriteBatch (GraphicsDevice);

			background = Content.Load<Texture2D> ("PrimaryBackground.png");

			//HighscoreSprite = Content.Load<SpriteFont> ("myFont");

			mainFrame = new Rectangle (0, WINDOW_HEIGHT - background.Height, background.Width, background.Height);

			newFrame = new Rectangle (0, WINDOW_HEIGHT - 2 * background.Height, background.Width, background.Height);

			X10 = new Player (Content, "spaceship1", 320, 460, WINDOW_WIDTH, WINDOW_HEIGHT);

			enemyManager = new EnemyManager (Content, "enemyDark", "Projectile1", WINDOW_HEIGHT, WINDOW_WIDTH);

			frameName [0] = "tile0";
			frameName [1] = "tile3";
			frameName [2] = "tile10";
			frameName [3] = "tile15";
			frameName [4] = "tile20";
			frameName [5] = "tile23";

			explosionManager = new ExplosionManager (Content, frameName, WINDOW_HEIGHT, WINDOW_WIDTH);

			bulletManager = new BulletManager (Content, enemyManager, explosionManager, X10, "Projectile1", WINDOW_HEIGHT, WINDOW_WIDTH);
		}


		protected override void UnloadContent()
		{

		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			/* if (X10 == null)
                this.Exit();*/

			keyboard = Keyboard.GetState();
			mouse = Mouse.GetState();

			//background
			mainFrame.Y += 1;
			newFrame.Y += 1;
			if (mainFrame.Y == 1)
			{
				newFrame = new Rectangle(0, -background.Height + 1, background.Width, background.Height);
			}
			if (mainFrame.Y == WINDOW_HEIGHT)
			{
				mainFrame = newFrame;
			}

			if (X10 != null)
			{
				if (mouse.LeftButton == ButtonState.Released && previousButtonState == ButtonState.Pressed)
				{
					X10.Fire(bulletManager);
				}
				previousButtonState = mouse.LeftButton;
			}
			X10.Update(keyboard);
			bulletManager.Update();

			enemyManager.Update(gameTime);
			explosionManager.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Silver);

			// TODO: Add your drawing code here
			spriteBatch.Begin();

			spriteBatch.Draw(background, mainFrame, Color.White);
			spriteBatch.Draw(background, newFrame, Color.White);
			if (X10 != null)
				X10.Draw(spriteBatch);
			enemyManager.Draw(spriteBatch);
			bulletManager.Draw(spriteBatch);
			explosionManager.Draw(spriteBatch);
			//spriteBatch.DrawString(HighscoreSprite, "Highscore: " + X10.highscore.ToString(), new Vector2(30, 30), Color.White); 

			spriteBatch.End();
			base.Draw(gameTime);

		}
	}
}