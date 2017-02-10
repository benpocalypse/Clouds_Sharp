using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsSharp
{
    public class Player
    {
        public int Score;
        public int Power;
        public int Lives;
        public int Delay;

        public Player()
        {
            Score = 0;
            Lives = 3;
            Power = 0;
            Delay = 0;
        }

        public void AddPower()
        {
            Power++;
            if (Power > Globals.MAXPOWER)
                Power = Globals.MAXPOWER;
        }

        public void SubtractPower()
        {
            Power--;
            if (Power < 0)
                Power = 0;
        }

        public void Update()
        {
            if (Score % Globals.LIFEUP == 1)
                Lives++;
        }
    }
}
