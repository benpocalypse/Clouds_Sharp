using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsSharp
{
    public class Particle
    {
        public int x;
        public int y;
        public int vel_x;
        public int vel_y;
        public int health;
        public bool active;

        public Particle()
        {
            /*
            x = xpos;
            y = ypos;
            int rando = 5;

            int temp = rand() % 4;
            if (temp == 0)
            {
                vel_x = rand() % rando;
                vel_y = rand() % rando;
            }

            if (temp == 1)
            {
                vel_x = -rand() % rando;
                vel_y = rand() % rando;
            }

            if (temp == 2)
            {
                vel_x = -rand() % rando;
                vel_y = -rand() % rando;
            }

            if (temp == 3)
            {
                vel_x = rand() % rando;
                vel_y = -rand() % rando;
            }

            health = rand() % 6;
            active = true;
            */
        }

        public void Update()
        {
            /*
            

            if(health <= 0)
                active = false;
            else
            {
                x += vel_x;
                y += vel_y;
                health--;
            }
            */
        }

        public void Destroy()
        {
            active = false;
        }
    }

    public class Explosion
    {
        public List<Particle> bits;
        public bool active;

        public Explosion(int x, int y)
        {
            bits = new List<Particle>();

            /*
            for (int i = 0; i < MAXPARTICLES; i++)
                bits[i].init(x, y);
            */

            active = true;
        }

        public void Update()
        {
            bool temp = false;
            for (int i = 0; i < bits.Count; i++)
            {
                bits[i].Update();
                if (bits[i].active)
                    temp = true;
            }

            if (temp)
                active = true;
            else
                active = false;

            temp = false;
        }

        public void Destroy()
        {
            for (int i = 0; i < bits.Count; i++)
                bits[i].Destroy();

            active = false;
        }
    }
}
