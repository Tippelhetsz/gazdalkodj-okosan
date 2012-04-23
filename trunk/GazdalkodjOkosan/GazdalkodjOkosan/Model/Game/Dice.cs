using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    class Dice
    {
        Random rand;

        public Dice() {
            rand = new Random();
        }

        public int Roll() {
            return rand.Next(1, 7);
        }
    }
}
