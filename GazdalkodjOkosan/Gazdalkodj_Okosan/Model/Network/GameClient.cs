using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using GazdalkodjOkosan.Model.Game;
using System.Net;
using Gazdalkodj_Okosan.Model.Network;

namespace Gazdalkodj_Okosan.Network
{
    /// <summary>
    /// Játékkliens kezelő típusa.
    /// </summary>
    public class GameClient
    {
        private GameServer _Server;
        private Socket _ClientSocket;
        private Byte[] _Buffer;

        /// <summary>
        /// Játékos azonosítójának lekérdezése.
        /// </summary>
        public Int32 PlayerId { get; private set; }
        /// <summary>
        /// Játékos ellenfeleinek lekérdezése, vagy beállítása.
        /// </summary>
        public List<GameClient> Opponents { get; set; }
        /// <summary>
        ///// Játékos nevének lekrédezése.
        ///// </summary>
        public String PlayerName { get; private set; }
        /// <summary>
        /// A kliens állapotának lekérdezése.
        /// </summary>
        public ClientState State { get; private set; }
        /// <summary>
        /// A kapcsolat fennállásának lekérdezése.
        /// </summary>
        public Boolean Connected { get { return (_ClientSocket != null); } }
        /// <summary>
        /// Kliens adatainak lekérdezése.
        /// </summary>
        public GameData gameData { get { return new GameData{  PlayerName= this.PlayerName, GameId = PlayerId.ToString(), PlayerAddress = ((IPEndPoint)_ClientSocket.RemoteEndPoint).Address.ToString() }; } }

        public GameClient(GameServer server, Int32 clientId, Socket socket)
        {
            _Server = server;
            PlayerId = clientId;
            _ClientSocket = socket;
            State = ClientState.Waiting;
            _Buffer = new Byte[256];
            Opponents = new List<GameClient>();

            _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceive_Completed), _ClientSocket);
        }

        /// <summary>
        /// Üzenetküldés.
        /// </summary>
        /// <param name="message">A küldendő szöveg.</param>
        public void SendMessage(String message)
        {
            try
            {
                Byte[] dataBytes = Encoding.UTF8.GetBytes(message);
                _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSend_Completed), _ClientSocket);
            }
            catch (Exception ex)
            {
                if (Opponents != null) // ha lecsatlakoztak a kliensek, akkor nyert
                    foreach (GameClient gameclient in Opponents)
                    {
                        gameclient.SendMessage(((Int32)MessageCode.GameOver).ToString());
                    }
                if (_ClientSocket != null)
                {
                    Console.Write("{0} kapcsolata lezárva.", PlayerName);
                    _ClientSocket.Close();
                    _ClientSocket = null;
                }
            }
        }

        private void MessageReceive_Completed(IAsyncResult iasyncResult)
        {
            try
            {
                Int32 byteCount = _ClientSocket.EndReceive(iasyncResult);
                String message = Encoding.UTF8.GetString(_Buffer, 0, byteCount);

                switch ((MessageCode)Int32.Parse(message.Substring(0, 1))) // kilvassuk az egész értéket, és megnézzük a felsorolási típusban
                {
                    case MessageCode.ConnectToServer:
                        PlayerName = message.Substring(1);
                        Console.WriteLine("{0} csatlakozott a szerverhez.", PlayerName);
                        SendMessage(((Int32)MessageCode.ConnectToServer).ToString() + PlayerId);
                        break;
                    case MessageCode.NewGame:
                        State = ClientState.CreatedNewGame;
                        Console.WriteLine("{0} új játékot indított.", PlayerName);
                        break;
                    case MessageCode.ConnectToGame:
                        Console.WriteLine("{0} csatlakozott a játékhoz.", PlayerName);
                        State = ClientState.Playing;
                        Int32 playerid = Int32.Parse(message.Substring(1));
                        GameClient opponent = _Server.Clients[playerid];
                        foreach (Int32 clientid in _Server.Clients.Keys)
                        {
                            if (clientid != playerid)
                            {
                                GameClient client = _Server.Clients[clientid];
                                client.Opponents.Add(opponent);
                                opponent.Opponents.Add(client);
                            }
                        }
                        opponent.SendMessage(((Int32)MessageCode.ConnectReceived).ToString() + PlayerName);
                        break;
                    case MessageCode.GameOver:
                        State = ClientState.Waiting;
                        foreach (GameClient client in Opponents)
                        {
                            client.State = ClientState.Waiting;
                            client.SendMessage(((Int32)MessageCode.GameOver).ToString() + PlayerName);
                        }
                        Opponents = null; // töröljük az ellenfeleket
                        break;
                }

                _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceive_Completed), _ClientSocket);

            }
            catch (Exception ex)
            {
                if (Opponents != null) // ha lecsatlakoztak a kliensek, akkor nyert
                {
                    foreach (GameClient gameclient in Opponents)
                    {
                        if (gameclient.Connected)
                        {
                            gameclient.State = ClientState.Waiting;
                            gameclient.SendMessage(((Int32)MessageCode.GameOver).ToString());
                        }
                    }
                }
                Opponents = null;

                if (_ClientSocket != null)
                {
                    Console.WriteLine("{0} kapcsolata lezárva.", PlayerName);
                    _ClientSocket.Close();
                    _ClientSocket = null;
                }
            }
        }

        private void MessageSend_Completed(IAsyncResult iasyncResult)
        {
            SocketError error;
            _ClientSocket.EndSend(iasyncResult, out error);
            if (error != SocketError.Success) // ha nem sikerült az üzenet küldése
                Console.Write("Hiba jelentkezett {0} kapcsolatában!", PlayerName);
        }
    }
}
