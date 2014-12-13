using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace SpaceSurvival
{
	public class PowerupManager
	{
		#region Fields
		const int MAX_NR_SHIELDS = 5;
		const int MAX_NR_WEAPONS = 5;
		int windowWidth;
		int windowHeight;

		string pushieldname, puweaponname;
		public PUShield[] shields;
		public PUWeapon[] weapons;
		int nr_shields;
		int nr_weapons;
		Player player;
		ContentManager contentManager;
		//public PUWeapon[] weapons;
		#endregion
		public PowerupManager (Player player, ContentManager contentManager, int windowWidth, int windowHeight, string pushieldname, string puweaponname )
		{
			this.player = player;
			this.contentManager = contentManager;
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
			this.pushieldname = pushieldname;
			this.puweaponname = puweaponname;
			//pushield = contentManager.Load<Texture2D> (pushieldname);
			//puweapon = contentManager.Load<Texture2D> (puweaponname);
			nr_shields = 0;
			nr_weapons = 0;
			shields = new PUShield[MAX_NR_SHIELDS];
			weapons = new PUWeapon[MAX_NR_WEAPONS];
		}
		public void Instantiate_Powerup(int x, int y, PUtype type)
		{
			if (type == PUtype.shield) {
				if (nr_shields < MAX_NR_SHIELDS) {
					nr_shields ++;
					for (int i = 0; i < nr_shields; i++) {
						if (shields [i] == null) {
							shields [i] = new PUShield (player, this, contentManager, pushieldname, windowWidth, windowHeight, x, y, i);
							break;
						}
					}
				}
			}
			else 
			if (type == PUtype.weapon) {
				if (nr_weapons < MAX_NR_WEAPONS) {
					nr_weapons ++;
					for (int i = 0; i < nr_weapons; i++) {
						if (weapons [i] == null) {
							weapons [i] = new PUWeapon (player, this, contentManager, puweaponname, windowWidth, windowHeight, x, y, i);
							break;
						}
					}
				}
			}
		}
		public void Update()
		{
			for (int i = 0; i < shields.Length; i++)
				if (shields[i]!=null)
					shields [i].Update();
			for (int i = 0; i < weapons.Length; i++)
				if (weapons[i]!=null)
					weapons [i].Update();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < shields.Length; i++)
				if (shields[i]!=null)
					shields [i].Draw (spriteBatch);
			for (int i = 0; i < weapons.Length; i++)
				if (weapons[i]!=null)
					weapons [i].Draw (spriteBatch);
		}
		public void Destroy(int index, PUtype type)
		{
			if (type == PUtype.shield) {
				nr_shields --;
				shields [index] = null;
			}
			else if (type == PUtype.weapon) {
				nr_weapons --;
				weapons [index] = null;
			}
		}
	}
}

