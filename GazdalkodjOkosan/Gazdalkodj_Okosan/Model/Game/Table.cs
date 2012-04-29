using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    class Table
    {
        public void CreateFields()
        {
            fields = new Field[40];
        }

        public void CreateLuckyCards()
        { 
        
        }

        private Field[] fields;
        private LuckyCard[] luckyCards;
    }
}
