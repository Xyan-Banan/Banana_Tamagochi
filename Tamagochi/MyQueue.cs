using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi
{
    class MyQueue
    {
        private int capacity;       // вместимость
        private int count_busy;     // количество занятых
        private KeyValuePair<Actions, Image>?[] elements;
        public KeyValuePair<Actions, Image>?[] Elements
        {
            get
            {
                return elements;
            }
        }

        public bool Enqueue(KeyValuePair<Actions, Image> pair)
        {
            if(count_busy == capacity)
            {
                return false;
            }

            elements[count_busy] = pair;
            count_busy++;
            return true;
        }

        public KeyValuePair<Actions,Image>? Dequeue()
        {

        }

    }
}
