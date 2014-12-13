using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace SpaceSurvival
{
    class Explosion
    {
        #region Fields;
		int BackgroundSpeed;
        int windowWidth;
        int windowHeight;
        int frame_number = 1;
        int elapsed_time = 0;
        const int NUM_FRAMES = 6;
        const int FRAME_TIME = 50;
        bool playing;
        ExplosionManager explosionManager;
        Texture2D[] frame = new Texture2D [NUM_FRAMES];
        Rectangle drawRectangle;
        int ID;
        #endregion
       
        #region Constructor
        public Explosion(ContentManager contentManager, ExplosionManager explosionManager, string[] frameName, 
                            int x, int y, int ID, int WindowWidth, int WindowHeight, int BackgroundSpeed)
        {
			this.BackgroundSpeed = BackgroundSpeed;
            this.windowWidth = WindowWidth;
            this.windowHeight = WindowHeight;
            this.playing = true;
            this.ID = ID;
            this.explosionManager = explosionManager;
            LoadContent(contentManager, x, y, frameName);
        }
        
        #endregion

        #region Public Methods
        public void Update(GameTime time)
        {
            if (playing)
            {
				drawRectangle.Y += BackgroundSpeed;
                elapsed_time += time.ElapsedGameTime.Milliseconds;
                if (elapsed_time > FRAME_TIME)
                {
                    elapsed_time = 0;
                    if (frame_number < NUM_FRAMES - 1)
                    {
                        frame_number++;
                    }
                    else
                        playing = false;
                }
            }
            if (playing == false)
                explosionManager.OnDeath(ID);
                
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
                spriteBatch.Draw(frame[frame_number], drawRectangle, Color.White);
        }

        #endregion

        #region Private Methods
        private void LoadContent(ContentManager contentManager, int x, int y, 
                                string[] frameName)
        {
            
            for (int i = 0; i < NUM_FRAMES; i++)
            {
                frame[i] = contentManager.Load<Texture2D>(frameName[i]);
            }
            drawRectangle = new Rectangle(x, y, frame[2].Width, frame[2].Height);

        }
        private void OnDeath(ExplosionManager explosionManager)
        {
            explosionManager.OnDeath(ID);
        }
        #endregion
    }
}
