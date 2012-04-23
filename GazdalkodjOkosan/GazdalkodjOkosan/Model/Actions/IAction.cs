using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Control;

namespace GazdalkodjOkosan.Model.Actions
{
    interface IAction
    {
        string Message { get;}
        bool Cond(IController engine);
        void Do(IController engine);
    }
}
