using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    internal class Stove : Component
    {
        public Stove(string name)
        {
            Name = name;
            State = false;
        }


        public override string Info()
        {
            return "Печь: " + Name;
        }
    }
}
