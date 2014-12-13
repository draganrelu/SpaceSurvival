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
    public class EnemyShip
    {
        public struct Point
        {
            public int x;
            public int y;
        }
        
        #region Fields
        int windowWidth, windowHeight;
        Point A;
		public int Life;
        double angle, speed_x, speed_y;
        Texture2D sprite;
        public Rectangle drawRectangle;
        public bool IsAlive;
        int ID;
		int movementtime;
		int elapsedtime = 0;
        
        EnemyManager enemyManager;

        #endregion

        #region Constructor
        public EnemyShip(ContentManager contentManager, EnemyManager enemyManager, string spriteName, 
                            int x, int y, int movementtime, int angle, double speed,
                            int ID, int WindowHeight, int WindowWidth)
		{
			LoadContent (contentManager, spriteName, x, y);
			this.windowHeight = WindowHeight;
			this.windowWidth = WindowWidth;
			this.movementtime = movementtime;
			A.x = x;
			A.y = y;
            
			this.angle = angle * 3.14 / 180;
			speed_x = speed * Math.Cos (this.angle);
			speed_y = speed * Math.Sin (this.angle);
 
			this.Life = 2;
			this.IsAlive = true;
			this.ID = ID;
			this.enemyManager = enemyManager;
		}
        #endregion

        #region Public Methods
        /*public bool Isthere()
        {
            if (Math.Abs(A.x - B.x) < epsilon && Math.Abs(A.y - B.y) < epsilon)
                return true;
            else return false;
        }*/
        
        public void Update(GameTime gameTime, Player player, ExplosionManager explosionManager, Output output)
        {
			if (Life > 0) 
			{
				if (elapsedtime < movementtime) {

					Move ();
					elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
				}
				CheckCollisionWithPlayer (player, explosionManager, output);
			}
			if (Life == 0) 
				IsAlive = false;
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
		public void CheckCollisionWithPlayer(Player player, ExplosionManager explosionManager, Output output)
		{
			if (drawRectangle.Intersects(player.drawRectangle))
			{
				this.IsAlive = false;
				Destroy (enemyManager);
				explosionManager.InstantiateExplosion (drawRectangle.X,
				                                      drawRectangle.Y);
				player.highscore += 25;
				output.WriteLine ("Your Highscore: " + player.highscore.ToString ());
				if (!player.IsShieldActive)
					player.OnDeath();
			}
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
            A.x = drawRectangle.Center.X;
            A.y = drawRectangle.Center.Y;
            drawRectangle.X += (int)speed_x;
            drawRectangle.Y += (int)speed_y;
			CheckBounds();
			BounceOffBounds();
        }
        private void Destroy(EnemyManager enemyManager)
        {
            enemyManager.OnDeathEnemy(ID);
        }
		private void BounceOffBounds()
		{
			//bounce left/right
			if (drawRectangle.X + drawRectangle.Width > windowWidth)
				speed_x = -speed_x;
			if (drawRectangle.X < 0)
				speed_x = -speed_x;

			//bounce bottom
			//if (drawRectangle.Y + drawRectangle.Height > windowHeight)
				//speed_y = -speed_y;
			//if (drawRectangle.Y < 0)
			//	speed_y = -speed_y;
		}
		private void CheckBounds()
		{
			if (drawRectangle.Top > windowHeight || drawRectangle.Left > windowWidth
				|| drawRectangle.Right < 0) {
				IsAlive = false;
				Destroy (enemyManager);
			}
		}
        
        #endregion
    }
}
