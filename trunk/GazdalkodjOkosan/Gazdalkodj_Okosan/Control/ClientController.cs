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
        public void CreateGame(Model.Game.Player[] players)
        {
            // todo: játék inicializálása kliens oldalon
            throw new NotImplementedException();
        }

        public void NextPlayer(int id = -1)
        {
            // todo: következő játékos emghatározása
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

        public Model.Game.Player CurrentPlayer
        {
            get { throw new NotImplementedException(); }
        }

        public Model.Game.Table Table
        {
            get { throw new NotImplementedException(); }
        }
    }
}
