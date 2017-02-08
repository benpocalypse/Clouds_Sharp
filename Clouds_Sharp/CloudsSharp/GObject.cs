using System;
namespace CloudsSharp
{
	public class GObject
	{
		public bool active;
		public int damage;
		public int health;
		public int texture_id;
		public int x, y;
		public int small_x, small_y;
		public int velocity_x;
		public int velocity_y;
		public int ai_type;
		public int bullet_type;
		public int shot_timer;
		public int angle;
		public int player;

		public GObject()
		{
		}

		public GObject(bool gactive, int gdamage, int ghealth, int gtexture_id,
	  				   int gx, int gy, int gsmall_x, int gsmall_y, int gvelocity_x,
	  				   int gvelocity_y)
		{
			active = gactive;
			damage = gdamage;
			health = ghealth;
			texture_id = gtexture_id;
			x = gx;
			y = gy;
			small_x = gsmall_x;
			small_y = gsmall_y;
			velocity_x = gvelocity_x;
			velocity_y = gvelocity_y;
			shot_timer = bullet_type = 0;
		}

		void Init(bool gactive, int gdamage, int ghealth, int gtexture_id,
	  		      int gx, int gy, int gsmall_x, int gsmall_y, int gvelocity_x,
	  			  int gvelocity_y)
		{
			active = gactive;
			damage = gdamage;
			health = ghealth;
			texture_id = gtexture_id;
			x = gx;
			y = gy;
			small_x = gsmall_x;
			small_y = gsmall_y;
			velocity_x = gvelocity_x;
			velocity_y = gvelocity_y;
			shot_timer = bullet_type = 0;
		}
	}
}
