using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Model.Actions
{
    class GoTo : IAction
    {
        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public bool Cond(Control.IController engine)
        {
            throw new NotImplementedException();
        }

        public void Do(Control.IController engine)
        {
            throw new NotImplementedException();
        }

        //
        private Field field;
    }
}
