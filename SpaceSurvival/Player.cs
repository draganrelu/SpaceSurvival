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
    class Player
    {
        #region Fields
        Texture2D sprite;
        int windowWidth;
        int windowHeight;
        public int highscore = 0;
        enum state { Dead, Alive };
        state STATE;
        public struct Point
        {
            public int x;
            public int y;
        }
        Point A; 
        #endregion  

        #region Properties
        public Rectangle drawRectangle;
        #endregion

        #region Constructor
        public Player(ContentManager contentManager, string spriteName, int x, int y, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
           
            LoadContent(contentManager, spriteName, x, y);
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
        #endregion

        #region Public Methods

        public void Fire(BulletManager bulletManager)
        {
            bulletManager.InstantiateBullet(drawRectangle.Center.X, drawRectangle.Top);
        }
            
        public void Update(KeyboardState keyboard)
        {
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
        }

        #endregion

        #region Private Methods

        private void LoadContent(ContentManager contentManager, string spriteName, int x, int y)
        {
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2, sprite.Width, sprite.Height);
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

        #endregion
    }
}
