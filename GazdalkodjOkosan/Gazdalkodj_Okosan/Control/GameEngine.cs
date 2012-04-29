using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Control
{
    class GameEngine : IController
    {
        #region Implement interface
        public int Roll()
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
        #endregion

        public Player Winner()
        {
            return null;
        }

        public void CreateGame(int players) {
            this.players = new Player[players];
            table = new Table();
            dice = new Dice();
        }

        private Player[] players;
        private Table table;
        private Dice dice;
    }
}
