using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SpaceSurvival
{
    public class EnemyManager
    {
        public struct Point
        {
            public int x;
            public int y;
        }
		int INITIAL_NR_ENEMY_SHIPS = 2;
        public const int MAX_NR_ENEMY_SHIPS = 50;
		public int nr_enemy_ships ;

        #region Fields

        int windowWidth;
        int windowHeight;
        ContentManager contentManager;
        Random rnd = new Random();
		int elapsedTime_CheckEnemyState = 0;
		int elapsedTime_difficulty = 0;
		int difficulty = 0;
		string spriteEnemyName;
		PowerupManager powerupManager;
        #endregion

        #region Properties
        public EnemyShip[] Enemy = new EnemyShip[MAX_NR_ENEMY_SHIPS];
        #endregion

        #region Constructor
        public EnemyManager(ContentManager contentManager, PowerupManager powerupManager, string spriteEnemyName, string spriteBulletName, int windowHeight, int windowWidth)
        {
            
            this.contentManager = contentManager;
			this.powerupManager = powerupManager;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
			this.spriteEnemyName = spriteEnemyName;
			nr_enemy_ships = INITIAL_NR_ENEMY_SHIPS;
			for (int i = 0; i < nr_enemy_ships; i++) 
			{
				Enemy[i] = new EnemyShip (contentManager, this, spriteEnemyName, rnd.Next (100, windowHeight - 100), 
				                          -100, rnd.Next (1000, 6000), rnd.Next (45, 135), rnd.Next (3, 5), 
				                          i, windowHeight, windowWidth);
			}
        }
        #endregion

        #region Public Methods
        public void Update(GameTime gameTime, Player player, ExplosionManager explosionManager, Output output)
        {
			//INSTANTIATING ENEMIES
			elapsedTime_CheckEnemyState += gameTime.ElapsedGameTime.Milliseconds;
			if (elapsedTime_CheckEnemyState > 4000)
			{
				if (IsEnemyNull()) 
					for (int i = 0; i < nr_enemy_ships; i++) 
					{
						Enemy[i] = new EnemyShip (contentManager, this, spriteEnemyName, rnd.Next (100, windowHeight - 100), 
					               -100, rnd.Next (1000, 6000), rnd.Next (45, 135), rnd.Next (3, 5), 
					               i, windowHeight, windowWidth);
					}
				elapsedTime_CheckEnemyState = 0;
			}
			elapsedTime_difficulty += gameTime.ElapsedGameTime.Milliseconds;
			if (elapsedTime_difficulty > 10000)
			{
				difficulty++;
				nr_enemy_ships = INITIAL_NR_ENEMY_SHIPS + difficulty;
				elapsedTime_difficulty = 0;
			}


            for (int i = 0; i < nr_enemy_ships; i++)
            {
                if (Enemy[i]!=null)
                    Enemy[i].Update(gameTime, player, explosionManager, output);
                
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nr_enemy_ships; i++)
                if (Enemy[i] != null) 
                    Enemy[i].Draw(spriteBatch);
            
        }
        public void OnDeathEnemy(int index)
        {
			if (!Enemy [index].IsAlive) {

				if (rnd.Next (0, 100) < 10)
				{
					if (rnd.Next(0,2) == 0)
						powerupManager.Instantiate_Powerup (Enemy [index].drawRectangle.X, 
						                                    Enemy [index].drawRectangle.Y, PUtype.shield);
					else
						powerupManager.Instantiate_Powerup (Enemy [index].drawRectangle.X, 
						                                    Enemy [index].drawRectangle.Y, PUtype.weapon);
				}
				Enemy [index] = null;	                               
			}
        }
		private bool IsEnemyNull()
		{
			for (int i = 0; i < nr_enemy_ships; i++)
				if (Enemy [i] != null)
					return false;
			return true;
		}
        
        
        #endregion
    }
}
