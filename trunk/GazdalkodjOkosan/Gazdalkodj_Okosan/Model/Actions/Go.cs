using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Go : IAction
    {
        string[] numbers { get { return new string[] { "", "egy", "két", "három" }; } }

        public Go(int fields, string message = "")
        {
            this.fields = fields;
            this.message = message;
        }
        public string Message
        {
            get { return message + "Lépj előre " + numbers[fields] + "mezőt!"; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            return engine.Step(fields);
        }

        //
        private int fields;
        private string message;
    }
}
