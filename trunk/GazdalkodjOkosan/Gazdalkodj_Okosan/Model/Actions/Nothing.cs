using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Nothing : IAction
    {
        public string Message
        {
            get { return ""; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine) {
            throw new NothingToDoException();
        }
    }
}
