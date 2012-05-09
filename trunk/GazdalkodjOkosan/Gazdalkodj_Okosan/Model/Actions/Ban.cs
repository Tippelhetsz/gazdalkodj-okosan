using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Ban : IAction
    {
        public Ban(int[] untilRoll, string message = "") {
            if (untilRoll.Length == 0)
            {
                this.message = "Fizetésképtelenné váltál! Sajnos kiestél a játékból!";
            }
            else
            {
                this.message = message + "Csak ";
                for (int i = 0; i < untilRoll.Length - 1; i++)
                {
                    this.message += untilRoll[i] + "-s, ";
                }
                this.message += "és " + untilRoll[untilRoll.Length - 1] + "-s dobással léphetsz tovább!";

                this.untilRoll = untilRoll;
                this.rounds = 0;
            }
        }

        public Ban(int rounds, string message = "")
        {
            this.message = message + rounds + " körből kimaradsz!";
            this.untilRoll = null;
        }

        public string Message
        {
            get { return message; }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            if (untilRoll == null)
            {
                engine.CurrentPlayer.RollsLeft -= rounds;
            }
            else {
                engine.CurrentPlayer.BanUntilRoll = untilRoll;
            }

            return new Nothing();
        }

        //
        private int[] untilRoll;
        private int rounds;
        private string message;
    }
}
