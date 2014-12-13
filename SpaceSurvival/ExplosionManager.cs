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
    public class ExplosionManager
    {
        public const int MAX_NR_EXPLOSIONS = 50;
        public const int NUM_FRAMES = 6;

        #region Fields
		int BackgroundSpeed;
        int windowWidth;
        int windowHeight;
        int nr_explosions = 0;
        string[] frameName;
        ContentManager contentManager;
        #endregion

        #region Properties
        Explosion[] explosion = new Explosion[MAX_NR_EXPLOSIONS];
        #endregion

        #region Constructor
        public ExplosionManager(ContentManager contentManager, string[] frameName,
                                int windowHeight, int windowWidth, int BackgroundSpeed)
        {
			this.BackgroundSpeed = BackgroundSpeed;
            this.frameName = frameName;
            this.contentManager = contentManager;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.contentManager = contentManager;
            
        }
        #endregion

        #region Public Methods
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < explosion.Length; i++)
                if (explosion[i] != null)
                    explosion[i].Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < explosion.Length; i++)
                if (explosion[i] != null)
                    explosion[i].Draw(spriteBatch);
        }
        public void InstantiateExplosion(int x, int y)
        {
            if (nr_explosions < MAX_NR_EXPLOSIONS)
            {
                nr_explosions++;
                for (int i = 0; i < explosion.Length; i++)
                {
					if (explosion [i] == null) 
					{
						explosion [i] = new Explosion (contentManager, this, frameName, x, y, i, windowWidth, windowHeight, BackgroundSpeed);
						i = explosion.Length;
					}
                }
            }
        }
        public void OnDeath(int index)
        {
            explosion[index] = null;
			nr_explosions--;
        }
        #endregion

    }
}
