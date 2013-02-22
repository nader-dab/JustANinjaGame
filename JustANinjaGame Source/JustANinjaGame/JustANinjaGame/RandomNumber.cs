using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustANinjaGame
{
    static class RandomNumber
    {
        private static Random rand;
        static RandomNumber()
        {
            rand = new Random();
        }
        public static int Generate(int numberX = 0, int numberY = 500)
        {
            return rand.Next(numberX, numberY + 1);
        }
    }
}
