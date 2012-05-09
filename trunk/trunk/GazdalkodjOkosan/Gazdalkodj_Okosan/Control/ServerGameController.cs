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

        /// <summary>
        /// Létrehozza a játék logikáját.
        /// </summary>
        /// <param name="players">Játékosok tömbje</param>
        public void CreateGame(Player[] players)
        {
            this.players = players;
            this.table = new Table();
            this.dice = new Dice();
            currentPlayer = -1;

            NextPlayer();
        }

        /// <summary>
        /// Dobás a kockával.
        /// </summary>
        /// <returns>
        /// Egy Go akciót ad vissza. Tehát a következő lehetőség, hogy lépjen annyit, amennyit dobott.
        /// Ha csak bizonyos dobásokkal léphet tovább a játékos, akkor Nothing akciót ad vissza.
        /// </returns>
        public IAction Roll()
        {
            int roll = dice.Roll();

            CurrentPlayer.RollsLeft--;

            if (players[currentPlayer].BanUntilRoll.Contains(roll))
            {
                players[currentPlayer].BanUntilRoll = new int[] { 1, 2, 3, 4, 5, 6 };

                return new Go(roll);
            }
            else {
                string message = "Csak ";
                for (int i = 0; i < players[currentPlayer].BanUntilRoll.Length - 1; i++)
                {
                    message += players[currentPlayer].BanUntilRoll[i] + "-s, ";
                }
                message += "és " + players[currentPlayer].BanUntilRoll[players[currentPlayer].BanUntilRoll.Length - 1] + "-s dobással léphetsz tovább!";

                return new Nothing(message);
            }
        }

        /// <summary>
        /// Az aktuális játékos lépését végzi el.
        /// </summary>
        /// <param name="fields">
        /// Ennyit lép a játékos
        /// </param>
        /// <returns>A célmező akcióját adja vissza</returns>
        public IAction Step(int fields)
        {
            if (CurrentPlayer.CurrentField + fields > Table.Fields.Length) {
                new StartField(false).Do(this);
                // todo: áthaladás a start menün, valamit jelezni a felhasználónak
            }

            return new Go(fields).Do(this);
        }

        /// <summary>
        /// Egy akciót hajt végre.
        /// </summary>
        /// <param name="action">A végrehajtandó akció</param>
        /// <returns>Az akció végrehajtásával előálló újabb akció</returns>
        public IAction DoAction(IAction action)
        {
            return action.Do(this);
        }

        /// <summary>
        /// A következő játékosra lép
        /// </summary>
        public void NextPlayer(int id = -1)
        {
            if (id < 0)
            {
                if (currentPlayer < 0)
                {
                    Random rand = new Random();
                    currentPlayer = rand.Next(0, players.Length);
                    CurrentPlayer.RollsLeft++;
                }
                else
                {
                    if (CurrentPlayer.RollsLeft <= 0)
                    {
                        currentPlayer = (currentPlayer + 1) % players.Length;
                        CurrentPlayer.RollsLeft++;
                    }
                }

                if (CurrentPlayer.BanUntilRoll.Length == 0)
                {
                    // A játékos már kiesett, ugorjuk át
                    NextPlayer();
                }
                else if (CurrentPlayer.RollsLeft <= 0)
                {
                    // A játékos nem léphet, mert kimarad valahány körből, ugorjuk át (esetleg küldjünk neki egy üzenetet)
                    NextPlayer();
                }
                else
                {
                    if (currentPlayer == MyID)
                    {
                        // todo: szerver oldali játékos jön, kiváltani valami yourTurn eseményt
                    }
                    else
                    {
                        // todo: kliens oldali játékos jön, üzenni neki hálózaton, hogy ő jön
                    }
                }
            }
        }

        public Table Table { get { return table; } }

        public Player CurrentPlayer { get { return players[currentPlayer]; } }
        #endregion

        /// <summary>
        /// Megadja a győztes játékost.
        /// </summary>
        /// <returns></returns>
        public Player Winner()
        {
            return null;
        }

        //
        private Player[] players;
        private int currentPlayer;
        private Table table;
        private Dice dice;

        private int MyID = 0;
    }
}
