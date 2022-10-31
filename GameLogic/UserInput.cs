using FountainOfObjects.Entities;

namespace FountainOfObjects.GameLogic
{
    internal static class UserInput
    {
        public static bool ParseInput(string input)
        {
            input = input.Trim().ToLower();

            if (input.EndsWith("north") || input.EndsWith("up"))
            {
                return Player.Move(CardinalDirection.North);
            }

            if (input.EndsWith("east") || input.EndsWith("right"))
            {
                return Player.Move(CardinalDirection.East);
            }

            if (input.EndsWith("south") || input.EndsWith("down"))
            {
                return Player.Move(CardinalDirection.South);
            }

            if (input.EndsWith("west") || input.EndsWith("left"))
            {
                return Player.Move(CardinalDirection.West);
            }

            if (input.Contains("enable"))
            {
                return Player.EnableFountain();
            }

            return false;
        }
    }
}
