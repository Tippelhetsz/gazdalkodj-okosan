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
            int step = field - engine.CurrentPlayer.currentField < 0 ? (engine.Table.Fields.Length - engine.CurrentPlayer.currentField + field) : (field - engine.CurrentPlayer.currentField);
            return engine.Step(step);
        }

        //
        private int field;
        private string message;
    }
}
