using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Fee : IAction
    {
        public Fee(int amount) {
            this.amount = amount;
        }

        #region interface
        public string Message
        {
            get { return message; }
            set { message = value + "Kapsz " + amount + ".- Ft-ot!"; }
        }

        public bool Cond(Control.IController engine)
        {
            if (_cond != null) return _cond.Invoke(engine);
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

        Cond _cond;
        Do _do;
    }
}
