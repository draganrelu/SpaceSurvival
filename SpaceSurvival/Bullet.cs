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
    class Bullet
    {
        public struct Point
        {
            public int x;
            public int y;
        }
        public enum BulletType { Player, Enemy };
        
        #region Fields
        const int SPEED1 = 10;
        const int SPEED2 = 2;
        Texture2D sprite;
        int windowWidth;
        int windowHeight;
        BulletType Typeofbullet;
        Point A;
        int ID;
        #endregion

        #region Properties 
        public Rectangle drawRectangle;
        #endregion

        #region Constructor

        public Bullet(ContentManager contentManager, string spriteName, int x, int y, BulletType type, int ID, int windowWidth, int windowHeight)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            LoadContent (contentManager, spriteName, x, y, type);
        }

        #endregion
       
        #region Properties

        public BulletType Type
        {
            get
            {
                return this.Typeofbullet;
            }
        }
        public Point coord
        {
            get
            {
                return this.A;
            }
        }
        #endregion

        #region Public Methods


        public bool Intersects(Rectangle rectangle)
        {
            if (drawRectangle.Intersects(rectangle)) return true;
            else
                return false;
        }
        public bool CheckInBounds()
        {
            return (drawRectangle.X > 0 && drawRectangle.X < (windowWidth - drawRectangle.Width) &&
                drawRectangle.Y > 0 && (drawRectangle.Y + drawRectangle.Height) < windowHeight);
        }
        public void Update()
        {
            if (CheckInBounds())
                Move();
            

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }
            

        #endregion

        #region Private Methods
        private void LoadContent(ContentManager contentManager, string spriteName, int x, int y, BulletType type)
        {
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width/2, y, sprite.Width, sprite.Height);
            this.Typeofbullet = type;
            this.A.x = drawRectangle.Center.X - sprite.Width/2;
            this.A.y = drawRectangle.Center.Y;
        }
        
        private void Move()
        {
            if (this.Typeofbullet == BulletType.Player)
                drawRectangle.Y -= SPEED1;
            else
                drawRectangle.Y += SPEED2;
        }
        /*private void Destroy(UnitManager unitManager)
        {
            unitManager.OnDeathBullet(ID);
        }*/
        #endregion

    }
}
