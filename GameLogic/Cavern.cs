using FountainOfObjects.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainOfObjects
{
    internal static class Cavern
    {
        private static List<List<Room>> Rooms = new List<List<Room>>();

        public static int PitRooms { get; private set; } = 0;
        public static int AmarokRooms { get; private set; } = 0;
        public static int MaelstromRooms { get; private set; } = 0;

        public static void BuildCavern(int n = 8)
        {
            for (int i = 0; i < n; i++)
            {
                List<Room> currentRow = new List<Room>();

                for (int j = 0; j < n; j++)
                {
                    currentRow.Add(new Room());
                }

                Rooms.Add(currentRow);
            }

            // Place the player at 0, 0
            // TODO: Have the player spawn at a random edge room of the cavern
            Rooms[0][0].isPlayerPresent = true;
            Rooms[0][0].visited = true;
        }

        public static void DisplayCavern()
        {
            Console.Write(" ");
            Console.Write(new String('/', Rooms[0].Count));
            Console.Write(" ");
            Console.WriteLine();

            foreach (List<Room> row in Rooms)
            {
                Console.Write("/");
                foreach (Room room in row)
                {
                    room.DisplayRoom();
                }
                Console.Write("/");
                Console.WriteLine();
            }

            Console.Write(" ");
            Console.Write(new String('/', Rooms[0].Count));
            Console.Write(" ");
            Console.WriteLine();
        }

        public static bool IsInBounds(Coordinate destination)
        {
            return (destination.X >= 0 && destination.X <= Rooms[0].Count - 1) &&
                (destination.Y >= 0 && destination.Y <= Rooms.Count - 1);
        }

        public static void UpdatePlayerPosition(Coordinate currentPosition, Coordinate destination)
        {
            Rooms[currentPosition.X][currentPosition.Y].isPlayerPresent = false;
            Rooms[destination.X][destination.Y].isPlayerPresent = true;
            Rooms[destination.X][destination.Y].visited = true;
        }
    }
}
