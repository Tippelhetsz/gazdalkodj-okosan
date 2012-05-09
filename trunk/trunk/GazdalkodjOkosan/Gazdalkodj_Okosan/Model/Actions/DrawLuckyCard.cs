using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class DrawLuckyCard : IAction
    {
        public DrawLuckyCard(string message = "") {
            this.message = message;
        }

        #region interface
        public string Message
        {
            get { return message + "Húzz egy szerencsekártyát!"; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            IAction action = engine.Table.LuckyCards[0].Action;
            engine.Table.LuckyCards.RemoveAt(0);
            return action;
        }
        #endregion

        private string message;
    }
}
