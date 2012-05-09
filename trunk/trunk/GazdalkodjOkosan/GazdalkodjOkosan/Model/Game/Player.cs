using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    class Player
    {
        private int UserID;
        private System.Drawing.Color color;
        private int Money;
        public int SavingsBook;
        public int BookToken;
        public int BanForRounds;
        public int RollsLeft;
        public EInsurance Insurance;
        public int[] BanUntilRoll;
        public int HouseLoan;
        public bool IsWinner()
        {
            return false;
        }

        private Field currentField;
        private House home;
    }
}
