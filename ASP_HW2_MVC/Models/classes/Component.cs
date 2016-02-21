using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    public abstract class Component
    {
        public string Name { get; set; }

        public bool State { get; set; }

        abstract public string Info();
                
    }
}
