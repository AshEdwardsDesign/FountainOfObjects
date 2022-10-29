using FountainOfObjects.GameLogic;

namespace FountainOfObjects.Entities
{
    internal static class Player
    {
        public static Coordinate PlayerPosition { get; private set; } = new Coordinate{ X = 0, Y = 0 };

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
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Destination is out of bounds.");
            Console.ResetColor();
            return false;
        }


    }
}
