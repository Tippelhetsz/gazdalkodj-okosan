using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Model.Actions
{
    class GoTo : IAction
    {
        public GoTo(int field, string message = "") {
            this.field = field;
            this.message = message;
        }

        public string Message
        {
            get {
                return message + "Lépj a(z) " + field + "-s mezőre!";
            }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            int step = field - engine.CurrentPlayer.CurrentField < 0 ? (engine.Table.Fields.Length - engine.CurrentPlayer.CurrentField + field) : (field - engine.CurrentPlayer.CurrentField);
            return engine.Step(step);
        }

        //
        private int field;
        private string message;
    }
}
