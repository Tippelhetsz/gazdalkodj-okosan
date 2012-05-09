using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class StartField : IAction
    {
        const int enterFee = 6000;
        const int moveThroughFee = 4000;

        public StartField(bool enter) {
            this.enter = enter;
            if (this.enter)
            {
                fee = new Fee(enterFee, "Beléptél a START mezőre.");
            }
            else {
                fee = new Fee(moveThroughFee, "Áthaladtál a START mezőn.");
            }
        }

        public string Message
        {
            get {
                return fee.Message;
            }
        }

        public bool Cond(Control.IController engine)
        {
            return fee.Cond(engine);
        }

        public IAction Do(Control.IController engine)
        {
            return fee.Do(engine);
        }

        //
        private bool enter;
        private Fee fee;
    }
}
