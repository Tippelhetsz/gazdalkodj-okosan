using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    public class Player
    {
        static int num = 0;
        public Player(string name, int id = -1) {
            if (id < 0) {
                UserId = num;
                num++;
            }
            UserName = name;
            furniture = new List<PieceOfFurniture>();

            Money = 20000;
            SavingsBook = 0;
            BookToken = 0;
            RollsLeft = 0;
            Insurance = EInsurance.Nothing;
            BanUntilRoll = new int[] { 1, 2, 3, 4, 5, 6 };
            HouseLoan = 0;
            currentField = 0;
        }

        public int Money { get; set; }
        public int SavingsBook;
        public int BookToken {
            get { return bookToken; }
            set
            {
                Money -= value - bookToken;
                bookToken = value;
            }
        }
        public int RollsLeft;
        public EInsurance Insurance;
        public int[] BanUntilRoll;
        public int HouseLoan;
        public House Home
        {
            get { return home; }
            set
            {
                if (value != null)
                {
                    home = value;
                    home.AddFurniture(furniture);
                    HouseLoan += home.Loan;
                    Money -= home.Price;
                }
            }
        }
        public bool IsWinner()
        {
            return false;
        }

        private int UserId;
        private string UserName;
        private List<PieceOfFurniture> furniture;
        public int currentField;
        private House home;
        private int bookToken;
    }
}
