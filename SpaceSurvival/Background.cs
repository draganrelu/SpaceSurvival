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
	public class Background
	{
		Texture2D background;
		Rectangle mainFrame, newFrame;
		int windowHeight;
		const int BACKGROUND_SPEED = 1;
		public Background (ContentManager contentManager, string spriteName, int windowHeight)
		{
			this.windowHeight = windowHeight;
			LoadContent (contentManager, spriteName);
		}
		public void Update()
		{
			mainFrame.Y += BACKGROUND_SPEED;
			newFrame.Y += BACKGROUND_SPEED;
			if (mainFrame.Y == 1)
			{
				newFrame = new Rectangle(0, -background.Height + 1, background.Width, background.Height);
			}
			if (mainFrame.Y == windowHeight)
			{
				mainFrame = newFrame;
			}
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(background, mainFrame, Color.White);
			spriteBatch.Draw(background, newFrame, Color.White);
		}
		private void LoadContent(ContentManager contentManager, string spriteName)
		{
			background = contentManager.Load<Texture2D> (spriteName);
			mainFrame = new Rectangle (0, windowHeight - background.Height, background.Width, background.Height);
			newFrame = new Rectangle (0, windowHeight - 2 * background.Height, background.Width, background.Height);
		}
		
	}
}

