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
            return dice.Roll();
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

        public Table Table { get { return table; } }

        public Player CurrentPlayer { get { return players[currentPlayer]; } }
        #endregion

        public Player Winner()
        {
            return null;
        }

        public void CreateGame(Player[] players) {
            this.players = players;
            this.table = new Table();
            this.dice = new Dice();
        }

        private Player[] players;
        private int currentPlayer;
        private Table table;
        private Dice dice;
    }
}
