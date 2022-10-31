using FountainOfObjects.GameLogic;

namespace FountainOfObjects.Entities
{
    internal static class Player
    {
        public static Coordinate PlayerPosition { get; private set; } = new Coordinate{ X = 0, Y = 0 };
        public static bool isAlive { get; private set; } = true;
        public static bool hasEscaped { get; private set; } = false;

        public static bool Move(CardinalDirection dir)
        {
            Coordinate destination = PlayerPosition;
            Coordinate currentPosition = PlayerPosition;

            switch (dir)
            {
                case CardinalDirection.North:
                    destination.X--;
                    break;
                case CardinalDirection.East:
                    destination.Y++;
                    break;
                case CardinalDirection.South:
                    destination.X++;
                    break;
                case CardinalDirection.West:
                    destination.Y--;
                    break;
                default:
                    break;
            }

            if (Cavern.IsInBounds(destination))
            {
                PlayerPosition = destination;
                Cavern.UpdatePlayerPosition(currentPosition, destination);
                if(!checkIfAlive())
                {
                    Player.isAlive = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have walked into a trap and died.");
                    Console.WriteLine("Press enter to try again.");
                    Console.ResetColor();
                    Console.ReadLine();
                }
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Destination is out of bounds.");
            Console.WriteLine("Press enter to continue.");
            Console.ResetColor();
            Console.ReadLine();
            return false;
        }

        private static bool checkIfAlive()
        {
            return Cavern.IsRoomSafe(PlayerPosition);
        }

        public static void StartOfNewGame()
        {
            isAlive = true;
            hasEscaped = false;
            PlayerPosition = new Coordinate { X = 0, Y = 0 };
        }

        public static bool EnableFountain()
        {
            if (Cavern.GetRoomAtCoordinate(PlayerPosition).entity is Fountain)
            {
                Fountain fountain = Cavern.GetRoomAtCoordinate(PlayerPosition).entity as Fountain;
                if (fountain.IsEnabled)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("The fountain is already activated.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return false;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You must be at the fountain to activate it.");
                    Console.ResetColor();
                    Console.ReadLine();
                    fountain.EnableFountain();
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You must be at the fountain to activate it.");
            Console.ResetColor();
            Console.ReadLine();
            return false;
        }


    }
}
