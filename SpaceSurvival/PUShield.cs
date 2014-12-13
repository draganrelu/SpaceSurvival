using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceSurvival
{
	public class PUShield : Powerup
	{
		Texture2D sprite;
		public int ID;
		public PUShield (Player player, PowerupManager powerupManager, ContentManager contentManager, string spritename, int windowWidth, int windowHeight, int x, int y, int ID)
		{
			this.player = player;
			this.powerupManager = powerupManager;
			this.type = PUtype.shield;
			this.ID = ID;
			IsAlive = true;
			sprite = contentManager.Load<Texture2D> (spritename);
			drawRect = new Rectangle (x, y, WIDTH, HEIGHT);
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
		}
		public override void Update()
		{
			if (IsInBounds ()) {
				Effect ();
				Move ();
			} else {
				IsAlive = false;
				powerupManager.Destroy (ID, type);
			}
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (sprite, drawRect, Color.White);
		}
		public void Effect()
		{
			if (drawRect.Intersects (player.drawRectangle)) 
			{
				if (player.shieldCharges < player.MAX_SHIELD_CHARGES)
					player.shieldCharges ++;
				this.IsAlive = false;
				powerupManager.Destroy (ID, this.type);
			}
		

		}
	}
}

