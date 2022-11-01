using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainOfObjects.GameLogic
{
    internal static class Config
    {
        public static int CavernSize { get; private set; } = 6;
        public static bool ShowVisitedRooms { get; private set; } = false;
        public static bool ShowNearbyThreats { get; private set; } = false;
    }
}
