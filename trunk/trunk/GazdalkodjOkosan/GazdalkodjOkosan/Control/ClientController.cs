using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Control
{
    /// <summary>
    /// A kliens oldalt vezérli. A hálózaton keresztül kell kommunikálnia.
    /// </summary>
    class ClientController : IController
    {
        public int Roll()
        {
            throw new NotImplementedException();
        }

        public Model.Actions.IAction Step(int fields)
        {
            throw new NotImplementedException();
        }

        public Model.Actions.IAction DoAction(Model.Actions.IAction action)
        {
            throw new NotImplementedException();
        }

        public void NextPlayer()
        {
            throw new NotImplementedException();
        }
    }
}
