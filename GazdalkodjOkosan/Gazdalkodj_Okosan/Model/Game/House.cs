using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    public class House
    {
        public House(int price, int loan) {
            this.price = price;
            this.loanPrice = loan;
            furniture = new List<PieceOfFurniture>();
        }

        public void AddFurniture(PieceOfFurniture piece) {
            this.furniture.Add(piece);
        }

        public void AddFurniture(ICollection<PieceOfFurniture> furniture)
        {
            this.furniture.AddRange(furniture);
        }

        public int Price { get { return price; } }
        public int Loan { get { return loanPrice; } }

        public bool IsComplete()
        {
            return false;
        }

        private int price;
        private int loanPrice;
        private List<PieceOfFurniture> furniture;
    }
}
