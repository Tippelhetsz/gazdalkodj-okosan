using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    /// <summary>
    /// Annak az akciója, hogy az aktuális akciósorozat befejeződött. Ezután hívni kell a NextPlayer metódust!
    /// Megjegyzés: Újra dobhatsz! mező esetén is egy Nothing akciót kapunk vissza, ekkor is hívni kell a NextPlayert.
    /// Ekkor a metódus újra az aktuális játékosnak adja a lehetőséget.
    /// </summary>
    class Nothing : IAction
    {
        public Nothing(string message = "") {
            Message = message;
        }

        public string Message
        {
            get;
            set;
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine) {
            throw new NothingToDoException();
        }
    }
}
