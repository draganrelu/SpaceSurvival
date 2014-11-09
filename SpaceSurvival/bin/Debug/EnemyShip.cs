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
    class EnemyShip
    {
        public struct Point
        {
            public int x;
            public int y;
        }
        
        #region Fields
        int windowWidth, windowHeight;
        Point A;
        Point B;
        double angle, epsilon = 2.1, speed_x, speed_y;
        Texture2D sprite;
        public Rectangle drawRectangle;
        public bool IsAlive;
        int ID;
        
        EnemyManager unitManager;
        #endregion

        #region Constructor
        public EnemyShip(ContentManager contentManager, EnemyManager unitManager, string spriteName, 
                            int x, int y, int x_dest, int y_dest, int angle, double speed,
                            int ID, int WindowHeight, int WindowWidth)
        {
            LoadContent(contentManager, spriteName, x, y);
            this.windowHeight = WindowHeight;
            this.windowWidth = WindowWidth;
            A.x = x;
            A.y = y;
            B.x = x_dest;
            B.y = y_dest;
            this.angle = angle*3.14/180;
            speed_x = speed * Math.Cos(this.angle);
            speed_y = speed * Math.Sin(this.angle);
 
            
            this.IsAlive = true;
            this.ID = ID;
            this.unitManager = unitManager;
        }
        #endregion

        #region Public Methods
        public bool Isthere()
        {
            if (Math.Abs(A.x - B.x) < epsilon && Math.Abs(A.y - B.y) < epsilon)
                return true;
            else return false;
        }
        
        public void Update()
        {
            Move();
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
        }
        private void Move()
        {
            if (!Isthere())
            {
                A.x = drawRectangle.Center.X;
                A.y = drawRectangle.Center.Y;
                drawRectangle.X += (int)speed_x;
                drawRectangle.Y += (int)speed_y;
               /* if (A.x > B.x)
                    drawRectangle.X -= (int)speed_x;
                if (A.x < B.x)
                        drawRectangle.X += (int)speed_x;
                if (A.y > B.y)
                    drawRectangle.Y -= (int)speed_y;
                if (A.y < B.y)
                    drawRectangle.Y += (int)speed_y;*/
            }
        }
        private void Destroy(EnemyManager unitManager)
        {
            unitManager.OnDeathEnemy(ID);
        }
        
        #endregion
    }
}
