using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainOfObjects.Entities
{
    internal class Fountain : Entity
    {
        public bool IsEnabled { get; private set; } = false;

        public void EnableFountain()
        {
            IsEnabled = true;
        }
    }
}
