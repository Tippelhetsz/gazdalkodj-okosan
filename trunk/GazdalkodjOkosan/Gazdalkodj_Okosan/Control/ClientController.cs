using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Model.Game;

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

        public IAction Roll()
        {
            throw new NotImplementedException();
        }

        public IAction Step(int fields)
        {
            throw new NotImplementedException();
        }

        public IAction DoAction(IAction action)
        {
            throw new NotImplementedException();
        }

        public void NextPlayer()
        {
            throw new NotImplementedException();
        }

        public Player CurrentPlayer
        {
            get { throw new NotImplementedException(); }
        }

        public Table Table
        {
            get { throw new NotImplementedException(); }
        }
    }
}
