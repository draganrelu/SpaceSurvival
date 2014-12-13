using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSurvival
{
    public class Player
    {
        #region Fields
        Texture2D sprite;
		Texture2D shield;
        int windowWidth;
        int windowHeight;
        public int highscore = 0;
        enum state { Dead, Alive };
        state STATE;
		Game1 game;
		bool shieldActive = false;
		const int SHIELD_DURATION = 3000;
		bool startTimer = false;
		int shieldTimer = 0;
		public int MAX_SHIELD_CHARGES = 6;
		public int MAX_WEAPON_POWER = 5;
		public int shieldCharges = 2;
		public int weaponpower;
		bool warnedbefore = false;
        public struct Point
        {
            public int x;
            public int y;
        }
        Point A; 
		Rectangle shieldrect;
        #endregion  

        #region Properties
        public Rectangle drawRectangle;

        #endregion

        #region Constructor
        public Player(ContentManager contentManager, Game1 game, string spriteName, string shieldname, int x, int y, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
			this.game = game;
			weaponpower = 1;
            LoadContent(contentManager, spriteName, shieldname, x, y);

        }
        #endregion 

        #region Properties
        
        public Point coord
        {
            get 
            { 
                return A;
            }
        }
        public bool IsAlive
        {
            get
            {
                if (this.STATE == state.Dead) return false;
                else return true;
            }
        }
		public bool IsShieldActive 
		{
			get 
			{
				return this.shieldActive;
			}
		}
		public int Highscore 
		{
			get 
			{
				return this.highscore;
			}
		}
        #endregion

        #region Public Methods

        public void Fire(BulletManager bulletManager)
		{
			bulletManager.InstantiateBullet (drawRectangle.Center.X, drawRectangle.Top);
      
		}
        public void Update(KeyboardState keyboard, GameTime gameTime, Output output)
        {
			ManageShield (keyboard, gameTime, output);
            Move(keyboard);
            BounceOffBounds();
        }
        public bool Intersects(Rectangle rectangle)
        {
            if (drawRectangle.Intersects(rectangle)) return true;
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			spriteBatch.Draw(sprite, drawRectangle, Color.White);
			if (IsShieldActive)
				spriteBatch.Draw (shield, shieldrect, Color.White);

        }

        #endregion

        #region Private Methods

        private void LoadContent(ContentManager contentManager, string spriteName, string shieldname, int x, int y)
        {
			shield = contentManager.Load<Texture2D> (shieldname);
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2, sprite.Width, sprite.Height);
			shieldrect = new Rectangle (x - shield.Width / 2, y - shield.Height / 2, shield.Width, shield.Height);
            this.A.x = drawRectangle.Center.X;
            this.A.y = drawRectangle.Center.Y;
            this.STATE = state.Alive;
        }
        private void Move(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Left))
            {
                drawRectangle.X -= 8;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                drawRectangle.X += 8;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                drawRectangle.Y -= 8;
            }

            if(keyboard.IsKeyDown(Keys.Down))
            {
                drawRectangle.Y += 8;
            }
        }
		private void ManageShield(KeyboardState keyboard, GameTime gameTime, Output output)
		{
			shieldrect.X = drawRectangle.Center.X - shieldrect.Width / 2;
			shieldrect.Y = drawRectangle.Center.Y - shieldrect.Height / 2;

			if (keyboard.IsKeyDown (Keys.A) && shieldTimer == 0 && shieldCharges > 0)
			{
				startTimer = true;
				shieldCharges --;
				output.WriteLine ("Shield activated. Charges left:" + shieldCharges.ToString ());

			}
			if (keyboard.IsKeyDown (Keys.A) && shieldTimer == 0 && shieldCharges == 0 && warnedbefore == false)
			{
				output.WriteLine ("No shield charges left.");
				warnedbefore = true;
			}

			if (startTimer == true) 
				shieldTimer += gameTime.ElapsedGameTime.Milliseconds;

			if (shieldTimer != 0)
			if (shieldTimer < SHIELD_DURATION)
				shieldActive = true;
			else {
				shieldTimer = 0;
				startTimer = false;
				shieldActive = false;
			}
			else 
				shieldActive = false;
		}
        private void BounceOffBounds()
        {
            //check boundaries Right Left
            if (drawRectangle.X + drawRectangle.Width > windowWidth)
                drawRectangle.X = windowWidth - drawRectangle.Width;
            if (drawRectangle.X < 0)
                drawRectangle.X = 0;

            //check boundaries Top Bottom
            if (drawRectangle.Y < 0)
                drawRectangle.Y = 0;
            if (drawRectangle.Y + drawRectangle.Height > windowHeight)
                drawRectangle.Y = windowHeight - drawRectangle.Height;
        }
		public void OnDeath()
		{
			game.KillPlayer();

		}

        #endregion
    }
}
