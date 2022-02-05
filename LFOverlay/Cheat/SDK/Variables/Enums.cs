using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFOverlay.Cheat.SDK.Variables
{
    class Enums
    {
        public enum SignOnState
        {
            NONE = 0,
            CHALLENGE = 1,
            CONNECTED = 2,
            NEW = 3,
            PRESPAWN = 4,
            SPAWN = 5,
            FULL = 6,
            CHANGELEVEL = 7
        }

        public enum Team
        {
            None = 0, Spectator = 1, Terrorists = 2, CounterTerrorists = 3
        }
    }
}
