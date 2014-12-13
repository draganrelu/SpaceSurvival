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
    public class BulletManager
    {
        public const int MAX_NR_BULLETS = 50;
        public int nr_bullets = 0;

        #region Fields
        string spriteBullet;
		Output output;
        ContentManager contentManager;
        EnemyManager enemyManager;
        ExplosionManager explosionManager;
        Player X10;
        int windowWidth;
        int windowHeight;
        
        #endregion

        #region Properties
        public Bullet[] bullet = new Bullet[MAX_NR_BULLETS];
        #endregion

        #region Constructor
        public BulletManager(Output output, ContentManager contentManager, EnemyManager enemyManager, ExplosionManager explosionManager, Player X10,
                             string spriteBulletName, int windowHeight, int windowWidth)
        {
			this.output = output;
            this.contentManager = contentManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
            this.spriteBullet = spriteBulletName;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.X10 = X10;

        }

        #endregion

        #region Public Methods
        public void Update()
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i] != null)
                {
                    bullet[i].Update();
                    if (bullet[i].CheckInBounds() == false)
                    {
                        nr_bullets--;
                        bullet[i] = null;
                    }
                    if (bullet[i] != null)
                        for (int j = 0; j < enemyManager.nr_enemy_ships; j++)
                        {
						   if (enemyManager.Enemy [j] != null)
							if (CheckCollision (bullet [i], enemyManager.Enemy [j]))
							{
								nr_bullets--;
								bullet [i] = null;
								
								if (enemyManager.Enemy [j].Life - X10.weaponpower <= 0) 
								{
									explosionManager.InstantiateExplosion (enemyManager.Enemy [j].drawRectangle.X,
									                                                                enemyManager.Enemy [j].drawRectangle.Y);
									X10.highscore += 25;
									output.WriteLine ("Your Highscore: " + X10.highscore.ToString ());
									enemyManager.Enemy [j].Life = 0;
									enemyManager.Enemy [j].IsAlive = false;
									enemyManager.OnDeathEnemy (j);
									break;
								}
								enemyManager.Enemy [j].Life -= X10.weaponpower;
								break;
								
							}
                        
                        }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i] != null)
                    bullet[i].Draw(spriteBatch);
            }
        }
        public void InstantiateBullet(int x, int y)
        {
            if (nr_bullets < MAX_NR_BULLETS)
            {
                nr_bullets++;
                for (int i = 0; i < bullet.Length; i++)
                {
                    if (bullet[i] == null)
                    {
                        bullet[i] = new Bullet(contentManager, spriteBullet, x, y, Bullet.BulletType.Player, i, windowWidth, windowHeight);
                        break;
                    }
                }
            }
        }
        public void OnDeathBullet(int index)
        {
            bullet[index] = null;
        }
        public bool CheckCollision(Bullet bullet, EnemyShip enemy)
        {
            if (bullet.drawRectangle.Intersects(enemy.drawRectangle))
                return true;
            else
                return false;
        }
        #endregion

    }
}
