using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;

namespace GazdalkodjOkosan.Model.Game
{
    class Table
    {
        public Table() {
            CreateFields();
            CreateLuckyCards();
        }

        private void CreateFields()
        {
            fields = new Field[40];
            fields[0] = new Field(0, new StartField(true));
            fields[1] = new Field(1, new Fine(50, "Szemeteltél!"));
            fields[2] = new Field(2, new DrawLuckyCard());
        }

        private void CreateLuckyCards()
        { 
        
        }

        public Field[] Fields { get { return fields; } }
        public List<LuckyCard> LuckyCards
        { 
            get {
                if (luckyCards.Count == 0) CreateLuckyCards();
                return luckyCards;
            }
        }

        private Field[] fields;
        private List<LuckyCard> luckyCards;
    }
}
