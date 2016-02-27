using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    public class TV : Component, IPowerable
    {
        public TV(string name)
        {
            Name = name;
            State = false;
        }

        private int channel;
        public int Channel
        {
            get
            {
                return channel;
            }
        }

        public void PowerOn()
        {
            State = true;
        }

        public void PowerOff()
        {
            State = false;
        }

        public virtual void NextChannel()
        {
            if (channel < 99)
            {
                channel += 1;
            }
            else
            {
                channel = 0;
            }
            State = true;
        }

        public virtual void PrevChannel()
        {
            if (channel > 0)
            {
                channel -= 1;
            }
            else
            {
                channel = 99;
            }
            State = true;
        }

        public override string Info()
        {

            string state;
            if (State)
            {
                state = "работает";
            }
            else
            {
                state = "не работает";
            }

            return "Телевизор: " + Name + ", состояние: " + state + ", канал: " + channel;
        }

    }
}
