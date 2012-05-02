using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Model.Actions
{
    class HouseShop : IAction
    {
        public HouseShop(int price, int loan, string message = "") {
            this.message = message;
            this.price = price;
            this.loan = loan;
        }

        public string Message
        {
            get { return message + "Fizess be " + price + ".- Ft-ot. A hátralevő " + loan + ".- Ft-ot körönként 2000.- Ft-os részletekben törlesztheted."; }
        }

        public bool Cond(Control.IController engine)
        {
            return engine.CurrentPlayer.Money >= price;
        }

        public IAction Do(Control.IController engine)
        {
            if (Cond(engine))
            {
                House house = new House(price, loan);
                engine.CurrentPlayer.Home = house;
                return new Nothing();
            }
            else { 
                return new Nothing();
            }
        }

        //
        private House house;
        private string message;
        private int price;
        private int loan;
    }
}
