using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class InsuranceShop : IAction
    {
        const int cseb = 150;
        const int home = 200;

        public InsuranceShop(string message = "") {
            this.message = message;
        }

        public string Message
        {
            get
            {
                string ret = message;
                if (ret != "") ret += "\n";

                ret += "Lakásbiztosítás\t" + home + ".- Ft\n";
                ret += "CSÉB-biztosítás\t" + cseb + ".- Ft";
                return ret;
            }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            return new Nothing();
        }

        //
        private string message;
    }
}
