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

        public Entity? entity { get; private set; }

        public void DisplayRoom()
        {
            char displayChar = ' ';

            if (visited)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }

            if (hasNearbyThreat)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }

            if (isPlayerPresent)
            {
                displayChar = 'P';
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }

            if (entity is CavernEntrance && visited)
            {
                displayChar = 'E';
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                if (isPlayerPresent)
                {
                    displayChar = 'P';
                }
            }

            if (entity is Fountain && visited)
            {
                displayChar = 'F';
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                if (isPlayerPresent)
                {
                    displayChar = 'P';
                }
            }

            Console.Write(displayChar);
            Console.ResetColor();
        }

        public void AddEntity(Entity entity)
        {
            if (this.entity == null)
            {
                this.entity = entity;
            }
        }
    }
}
