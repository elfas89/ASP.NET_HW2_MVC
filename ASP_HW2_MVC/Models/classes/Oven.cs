using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    public class Oven : Stove, IOpenable
    {
        public Oven(string name, int mintemp, int maxtemp)
            : base(name)
        {
            Name = name;
            State = false;
            minTemper = mintemp;
            maxTemper = maxtemp;
        }

        private int temperature;
        private int minTemper;
        private int maxTemper;

        public int Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                if (value >= minTemper && value <= maxTemper)
                {
                    temperature = value;
                }
            }
        }

        private bool doorOpened;



        public void Open()
        {
            doorOpened = true;
        }

        public void Close()
        {
            doorOpened = false;
        }

        public void SetTemper(int temper)
        {
            Temperature = temper;
        }

        public override string Info()
        {
            string door;
            if (doorOpened)
            {
                return "Духовка: " + Name + " - закройте дверцу духовки!";
            }
            else
            {
                door = "дверца закрыта, текущая температура " + temperature;
            }

            return "Духовка: " + Name + ", " + door;
        }
    }
}
