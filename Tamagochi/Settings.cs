using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi
{
    class Settings
    {
        public static Scale eat;
        public static Scale sleep;
        public static Scale happy;
        public static Scale clear;
        public static Scale hp;
        public static int sub;
        public static int add;

        public Settings()
        {
            eat = new Scale();
            sleep = new Scale(); 
            happy = new Scale();
            clear = new Scale();
            hp = new Scale();
            add = 15;
            sub = 8;
        }
    }
}
