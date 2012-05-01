using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Control;

namespace GazdalkodjOkosan.Model.Actions
{
    interface IAction
    {
        string Message { get; }
        bool Cond(IController engine);
        IAction Do(IController engine);
    }

    delegate bool Cond(IController engine);
    delegate void Do(IController engine);

    class NothingToDoException : Exception {
        public NothingToDoException() : base("This is a Nothing action. You have to call NextPlayer function!") {}
    }
}
