using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsSharp
{
    public class Powerup
    {
        public bool active;
        public int x;
        public int y;
        public int health;
        public int texture_id;
        public int angle;

        public Powerup(int tx, int ty)
        {
            x = tx;
            y = ty;
            active = false;
            health = 150;
            angle = 0;
        }

        public void Update()
        {
            y = y + (int)(1.2 * Math.Sin(angle * (Math.PI / 180)));
            angle += 20;

            if (angle >= 360)
                angle = 0;

            health--;

            if ((health <= 0) || (x < 0))
                active = false;
            else
                active = true;
        }
    }

    public class PowerupManager
    {
        public List<Powerup> powerups;

        public PowerupManager(int texid)
        {
            powerups = new List<Powerup>();
            for (int i = 0; i < 100; i++)
            {
                powerups.Add(new Powerup(-10, -10));
                powerups[0].texture_id = texid;
            }
        }


        public void AddPowerup(int px, int py)
        {
            bool added = false;

            for (int i = 0; i < powerups.Count; i++)
            {
                if ((!added) && (!powerups[i].active))
                {
                    added = true;
                    powerups[i] = new Powerup(px, py);
                    powerups[i].active = true;
                }
            }
            added = false;
        }

        public void Update()
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                powerups[i].x -= Globals.SCROLLSPEED;
                powerups[i].Update();
            }
        }
    }
}
