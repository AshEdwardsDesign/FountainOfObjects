using FountainOfObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FountainOfObjects
{
    internal class Room
    {
        public bool visited = false;
        public bool hasNearbyThreat = false;
        public bool isPlayerPresent = false;

        private Entity? entity;

        public void DisplayRoom()
        {
            char displayChar = ' ';

            if (visited)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            } else if (hasNearbyThreat)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }

            if (isPlayerPresent)
            {
                displayChar = 'P';
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }

            Console.Write(displayChar);
            Console.ResetColor();
        }
    }
}
