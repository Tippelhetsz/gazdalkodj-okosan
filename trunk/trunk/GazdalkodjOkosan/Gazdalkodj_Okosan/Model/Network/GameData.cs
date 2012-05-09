using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gazdalkodj_Okosan.Model.Network
{
    /// <summary>
    /// Üzenetkódok felsoroló típusa.
    /// </summary>
    public enum MessageCode { Connected, ConnectToServer, StartGame, NextPlayer, Step }

    /// <summary>
    /// A kliens állapotainak felsoroló típusa.
    /// </summary>
    public enum ClientState { Connected, Waiting, CreatedNewGame, Playing }

    /// <summary>
    /// Játék adatainak típusa.
    /// </summary>
    public struct GameData
    {
        /// <summary>
        /// Játék azonosítójának lekérdezése, vagy beállítása.
        /// </summary>
        public String GameId { get; set; }
        /// <summary>
        /// Játékos nevének lekérdezése, vagy beállítása.
        /// </summary>
        public String PlayerName { get; set; }
        /// <summary>
        /// Játékos címének lekérdezése, vagy beállítása.
        /// </summary>
        public String PlayerAddress { get; set; }

        public override String ToString() { return GameId + "|" + PlayerName + "|" + PlayerAddress; }

        public static GameData Parse(String gameDataString)
        {
            String[] data = gameDataString.Split('|');
            return new GameData { GameId = data[0], PlayerName = data[1], PlayerAddress = data[2] };
        }
    }
}
