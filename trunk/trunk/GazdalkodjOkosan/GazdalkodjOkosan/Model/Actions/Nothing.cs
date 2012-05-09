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

        public void Do(Control.IController engine) {}
    }
}
