using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsSharp
{
    public sealed class Globals
    {
        private static System.Threading.Timer tmrUpdate;
        private volatile Boolean bCommunicating;

        public const int iCommTime = 25;

        //Misc defines for convenince
        public const int MAXSOUNDS = 20;
        public const int MAXBULLETS = 50;
        public const int MAXEXPLOSIONS = 10;
        public const int MAXPARTICLES = 100;
        public const int MAXPOWERUPS = 50;
        public const int MAXPOWER =         10;
        public const int RESPAWNTIME =      45;
        public const int LIFEUP =           500;
        public const int PLAYERBASEHEALTH = 10;
        public const int PLAYERBASEDAMAGE = 3;
        public const int PLAYERFIRINGRATE = 8;
        public const int SCROLLSPEED =      1;
        public const int PLAYERSPEED =      3;
        public const int GAMEDEBUG =		0;
        public const int SHOWFPS =          1;

        //Gamestate defines
        public const int GAMEOVER = 0;
        public const int PLAYING  = 1;
        public const int PAUSED   = 2;
        public const int TITLE    = 3;
        public const int CINEMA   = 4;
        public const int LEVELOVER= 5;

        //Input defines
        public const int NONE     = 0;
        public const int UP       = 1;
        public const int DOWN     = 2;
        public const int LEFT     = 3;
        public const int RIGHT    = 4;
        public const int UPLEFT   = 5;
        public const int UPRIGHT  = 6;
        public const int DOWNLEFT = 7;
        public const int DOWNRIGHT= 8;
        public const int FIRE     = 9;
        public const int ESCAPE   = 10;
        public const int YES      = 11;
        public const int NO       = 12;
        public const int SPACE    = 13;

        private static readonly Globals gInstance = new Globals();
        public static Globals Instance
        {
            get
            {
                return gInstance;
            }
        }

        static Globals()
        {
            // Do nothing here - this will make our class a singleton.
        }

        private Globals()
        {
            // Actual constructor
            bCommunicating = false;
        }
    }
}
