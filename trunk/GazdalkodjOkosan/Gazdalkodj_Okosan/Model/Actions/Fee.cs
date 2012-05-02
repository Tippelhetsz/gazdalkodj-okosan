using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Fee : IAction
    {
        public Fee(int amount, string message = "", FCond cond = null) {
            this.amount = amount;
            this.message = message;
            this.cond = cond;
        }

        #region interface
        public string Message
        {
            get { return message + "Kapsz " + amount + ".- Ft-ot!"; }
        }

        public bool Cond(Control.IController engine)
        {
            if (cond != null) return cond.Invoke(engine);
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            if (Cond(engine))
            {
                engine.CurrentPlayer.Money += amount;
            }

            return new Nothing();
        }
        #endregion

        // Data
        private int amount;
        private string message;

        FCond cond;
    }
}
