#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
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

		//Graphics eu;
		//Bitmap image = new Bitmap (WINDOW_WIDTH, WINDOW_HEIGHT);



		public const int NUM_FRAMES = 6;
		public const int WINDOW_WIDTH = 640;
		public const int WINDOW_HEIGHT = 475;
		string[] frameName = new string[NUM_FRAMES];
		public bool IsRunning;
		//Image boss;
		//boss = new Image.FromFile("eu");
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Background background;
		const int BACKGROUND_SPEED = 1;
		Player X10;
		KeyboardState keyboard;
		Output output = new Output ();
		bool previousButtonStateIsPressed = false;
		PowerupManager powerupManager;
		EnemyManager enemyManager;
		BulletManager bulletManager;
		ExplosionManager explosionManager;
		int GAME_OVER_PAUSE = 4000;
		int pause = 0;
		Rectangle gameover_rect; 
		Texture2D gameover_image;
		public Game1()

		{

			graphics = new GraphicsDeviceManager(this);
			IsRunning = true;
			//eu = Graphics.FromImage (image);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			graphics.IsFullScreen = false;
			gameover_rect = new Rectangle(120,150,400,175);
		}
		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{

			spriteBatch = new SpriteBatch (GraphicsDevice);
			gameover_image = Content.Load<Texture2D> ("game_over.png");
			background = new Background (Content, "PrimaryBackground", WINDOW_HEIGHT);
			X10 = new Player (Content, this, "spaceship1", "shield2" , 320, 460, WINDOW_WIDTH, WINDOW_HEIGHT);
			powerupManager = new PowerupManager (X10, Content, WINDOW_WIDTH, WINDOW_HEIGHT, "shieldpu.png", "weaponpu.png");
			enemyManager = new EnemyManager (Content, powerupManager,"enemyDark", "Projectile1", WINDOW_HEIGHT, WINDOW_WIDTH);
			frameName [0] = "tile0";
			frameName [1] = "tile3";
			frameName [2] = "tile10";
			frameName [3] = "tile15";
			frameName [4] = "tile20";
			frameName [5] = "tile23";
			explosionManager = new ExplosionManager (Content, frameName, WINDOW_HEIGHT, WINDOW_WIDTH, BACKGROUND_SPEED);
			bulletManager = new BulletManager (output, Content, enemyManager, explosionManager, X10, "Projectile1", WINDOW_HEIGHT, WINDOW_WIDTH);
		}
		 

		protected override void UnloadContent()
		{
		}
		public void KillPlayer()
		{
			X10 = null;
		}
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (X10 == null) 
			{	 

				IsRunning = false;
				pause += gameTime.ElapsedGameTime.Milliseconds;

				if (pause > GAME_OVER_PAUSE)
				{
					output.console_file.Close ();
					this.Exit ();
				}
			} 
			else 
			{
				keyboard = Keyboard.GetState ();
				background.Update ();
				if (X10 != null) 
				{
					if (keyboard.IsKeyDown (Keys.Space) == false && previousButtonStateIsPressed == true) {
						X10.Fire (bulletManager);
					}
					previousButtonStateIsPressed = keyboard.IsKeyDown (Keys.Space);
					X10.Update (keyboard,gameTime,output);
				}
				bulletManager.Update ();
				enemyManager.Update (gameTime, X10, explosionManager, output);
				explosionManager.Update (gameTime);
				powerupManager.Update ();
				
				base.Update (gameTime);
			}
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Silver);

			// TODO: Add your drawing code here
			spriteBatch.Begin();


			background.Draw (spriteBatch);
			if (X10 != null)
				X10.Draw(spriteBatch);
			powerupManager.Draw (spriteBatch);
			enemyManager.Draw(spriteBatch);
			bulletManager.Draw(spriteBatch);
			explosionManager.Draw(spriteBatch);
			if (!IsRunning)
				spriteBatch.Draw (gameover_image, gameover_rect, Color.White);
			spriteBatch.End();
			base.Draw(gameTime);
		
		
		}
	}
}