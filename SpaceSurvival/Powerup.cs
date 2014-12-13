using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace SpaceSurvival
{
	public class Powerup
	{
		#region Fields
		public int speed = 2;
		public int WIDTH = 35;
		public int HEIGHT = 35;
		public int windowWidth;
		public int windowHeight;
		public bool IsAlive;
		public Rectangle drawRect;
		public Player player;
		public PUtype type;
		public PowerupManager powerupManager;

		#endregion
		#region Public Methods
		public Powerup ()
		{
		}
		public virtual void Update()
		{

			if (IsInBounds ())
				Move ();


		}
		public void Move ()
		{
			drawRect.Y += speed;
		}
		public bool IsInBounds ()
		{

			if (drawRect.Left > 0 && drawRect.Right < windowWidth && drawRect.Top > 0 && drawRect.Bottom < windowHeight)
				return true;
			else 
				return false;
		}
		#endregion



	}
}

