using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    public class Player
    {
        public Player(string name) {
            UserName = name;
        }

        private string UserName;
        public int Money { get; set; }
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
