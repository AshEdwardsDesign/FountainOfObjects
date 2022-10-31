using FountainOfObjects.Entities;
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

        public static void BuildCavern(int n = 8)
        {
            Rooms.Clear(); // Clear the existing cavern before building it (i.e. in case of restarting game after death)

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
            Rooms[0][0].AddEntity(new CavernEntrance());

            // Place a pit near the start (testing threat detection)
            // TODO: Add random threat placement
            Rooms[2][2].AddEntity(new Pit());

            // Place the fountain
            // TODO: Add random fountain placement
            Rooms[4][4].AddEntity(new Fountain());
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

        public static void SenseNearbyEntities(Coordinate playerPosition)
        {
            StringBuilder response = new StringBuilder("");
            List<Entity> threats = new List<Entity>();
            List<Room> adjacents = GetAdjacentRooms(playerPosition);

            foreach (Room room in adjacents)
            {
                if (room.entity is not null)
                {
                    threats.Add(room.entity);
                }
            }

            if (GetRoomAtCoordinate(playerPosition).entity is CavernEntrance)
            {
                response.Append("You see light in this room coming from outside the cavern. This is the entrance.\n");
            }

            if (GetRoomAtCoordinate(playerPosition).entity is Fountain)
            {
                Fountain fountain = (Fountain)GetRoomAtCoordinate(playerPosition).entity;

                if (fountain.IsEnabled)
                {
                    response.Append("You hear the rushing waters from the Fountain Of Objects. It has been reactivated!\n");
                } else
                {
                    response.Append("You hear water dripping in this room. The Fountain Of Objects is here!\n");
                }

            }


            if (threats.Any(x => x is Pit))
            {
                response.Append("You feel a draft. There is a pit in a nearby room.\n");
            }

            if (threats.Any(x => x is Amarok))
            {
                response.Append("You can smell the rotten stench of an Amarok in a nearby room.\n");
            }

            if (threats.Any(x => x is Maelstrom))
            {
                response.Append("You hear the growling and groaning of a Maelstrom nearby\n");
            }

            if (response.ToString() == "")
            {
                response.Append("You sense nothing in the nearby rooms.\n");
            }

            if (threats.Count > 0)
            {
                GetRoomAtCoordinate(playerPosition).hasNearbyThreat = true;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        private static List<Room> GetAdjacentRooms(Coordinate playerPosition)
        {
            List<Room> adjacents = new List<Room>();

            if (playerPosition.X > 0)
            {
                adjacents.Add(Rooms[playerPosition.X - 1][playerPosition.Y]); // North
            }

            if (playerPosition.X <= Rooms[0].Count - 2)
            {
                adjacents.Add(Rooms[playerPosition.X + 1][playerPosition.Y]); // South
            }

            if (playerPosition.Y > 0)
            {
                adjacents.Add(Rooms[playerPosition.X][playerPosition.Y - 1]); // West
            }

            if (playerPosition.Y <= Rooms.Count - 2)
            {
                adjacents.Add(Rooms[playerPosition.X][playerPosition.Y + 1]); // East
            }

            if (playerPosition.X > 0 && playerPosition.Y > 0)
            {
                adjacents.Add(Rooms[playerPosition.X - 1][playerPosition.Y - 1]); // North-West
            }

            if (playerPosition.X <= Rooms[0].Count - 2 && playerPosition.Y > 0)
            {
                adjacents.Add(Rooms[playerPosition.X + 1][playerPosition.Y - 1]); // North-East
            }

            if (playerPosition.X > 0 && playerPosition.Y <= Rooms.Count - 2)
            {
                adjacents.Add(Rooms[playerPosition.X - 1][playerPosition.Y + 1]); // South-West
            }

            if (playerPosition.X <= Rooms[0].Count - 2 && playerPosition.Y <= Rooms.Count - 2)
            {
                adjacents.Add(Rooms[playerPosition.X + 1][playerPosition.Y + 1]); // South-East
            }

            return adjacents;
        }

        public static Room GetRoomAtCoordinate(Coordinate playerPosition)
        {
            return Rooms[playerPosition.X][playerPosition.Y];
        }

        public static bool IsRoomSafe(Coordinate playerPosition)
        {
            return Rooms[playerPosition.X][playerPosition.Y].entity switch
            {
                Amarok => false,
                Pit => false,
                Maelstrom => false,
                _ => true
            };
        }
    }
}
