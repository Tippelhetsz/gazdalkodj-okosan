using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class PlusRolls : IAction
    {
        string[] numbers { get { return new string[] { "", "egyszer", "kétszer", "háromszor" }; } }

        public PlusRolls(int rolls, string message = null) {
            this.rolls = rolls;
            this.message = message;
        }

        public string Message
        {
            get { return message + "Dobhatsz még " + numbers[rolls] + "!"; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            engine.CurrentPlayer.RollsLeft += rolls;
            return new Nothing();
        }

        //
        private int rolls;
        private string message;
    }
}
