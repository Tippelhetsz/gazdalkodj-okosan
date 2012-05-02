using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class BookShop : IAction
    {
        public BookShop(int price, string message = "") {
            this.price = price;
            this.message = message;
        }

        public string Message
        {
            get { return message + (message == "" ? "" : "\n") + price + ".-Ft"; }
        }

        public bool Cond(Control.IController engine)
        {
            return engine.CurrentPlayer.Money >= price;
        }

        public IAction Do(Control.IController engine)
        {
            if (Cond(engine))
            {
                engine.CurrentPlayer.BookToken += price;
            }

            return new Nothing();
        }

        //
        private string message;
        private int price;
    }
}
