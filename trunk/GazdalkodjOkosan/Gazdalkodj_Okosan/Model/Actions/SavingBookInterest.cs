using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class SavingBookInterest : IAction
    {
        public SavingBookInterest(int percent, string message = "") {
            this.percent = percent;
            this.message = message;
        }

        public string Message
        {
            get { return message + "Betéteid után " + percent + "% kamatot kapsz!"; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            return new Fee((int)((double)engine.CurrentPlayer.SavingsBook * (double)percent / 100.0)).Do(engine);
        }

        //
        private int percent;
        private string message;
    }
}
