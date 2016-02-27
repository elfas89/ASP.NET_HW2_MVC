using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    public class Fridge : Component, IPowerable, IOpenable
    {
        private FridgeModes mode;
        private bool doorOpened;

        public FridgeModes Mode { get; set; }

        public Fridge (string name) 

        {
            Name = name;
            this.mode = FridgeModes.normal;
        }

        public void PowerOn()
        {
            if (!State) { 
            State = true;
            mode = FridgeModes.normal;
            }
        }

        public void PowerOff()
        {
            if (State)
            {
            State = false;
            mode = FridgeModes.south;
            }
        }

        public void Open()
        {
            doorOpened = true;
        }

        public void Close()
        {
            doorOpened = false;
        }


        public void Normal()
        {
            if (State)
            {
                mode = FridgeModes.normal;
            }
        }

        public void North()
        {
            if (State)
            {
                mode = FridgeModes.north;
            }
        }

        public void South()
        {
            if (State)
            {
                mode = FridgeModes.south;
            }
        }


        public override string Info()
        {
            if (doorOpened)
            {
                return "Холодильник: " + Name + " - закройте дверцу холодильника!";
            }

            string state;
            if (State)
            {
                state = "работает";
            }
            else
            {
                return "Холодильник: " + Name + " выключен, продукты портятся!";
            }

            string mode;
            if (this.mode == FridgeModes.normal)
            {
                mode = "нормальный";
            }
            else if (this.mode == FridgeModes.north)
            {
                mode = "заморозки к чертям";
            }
            else
            {
                mode = "разморозки к лужам";
            }

            return "Холодильник: " + Name + ", состояние: " + state + ", режим: " + mode;
        }

    }
}
