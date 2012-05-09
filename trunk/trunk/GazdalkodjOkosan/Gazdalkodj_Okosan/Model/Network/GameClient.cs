using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using GazdalkodjOkosan.Model.Game;
using System.Net;
using Gazdalkodj_Okosan.Model.Network;
using GazdalkodjOkosan.Control;

namespace Gazdalkodj_Okosan.Network
{
    /// <summary>
    /// Játékkliens kezelő típusa.
    /// </summary>
    public class GameClient
    {
        private GameServer _Server;
        private GameEngine _Controller;
        private Socket _ClientSocket;
        private Byte[] _Buffer;
        private Int32 PlayerCount;

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
        public GameData gameData { get { return new GameData { PlayerName = this.PlayerName, GameId = PlayerId.ToString(), PlayerAddress = ((IPEndPoint)_ClientSocket.RemoteEndPoint).Address.ToString() }; } }

        public GameClient(GameServer server, Int32 clientId, Socket socket)
        {
            _Server = server;
            _Controller = new GameEngine();
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
                        Console.WriteLine("Server : {0} csatlakozott a szerverhez.", PlayerName);
                        foreach (Int32 clientid in _Server.Clients.Keys)
                        {
                            if (clientid != PlayerId)
                            {
                                GameClient client = _Server.Clients[clientid];
                                client.SendMessage(((Int32)MessageCode.ConnectToServer).ToString() + PlayerName);
                            }
                        }
                        SendMessage(((Int32)MessageCode.Connected).ToString() + PlayerName);
                        break;
                    case MessageCode.StartGame:
                        State = ClientState.CreatedNewGame;
                        Console.WriteLine("Server: {0} új játékot indított.", PlayerName);
                        PlayerCount = _Server.Clients.Values.Count;
                        String names = "";
                        foreach (GameClient gameclient in _Server.Clients.Values)
                        {
                            names += gameclient.PlayerName + "|";
                        }
                        SendMessageAll(((Int32)MessageCode.StartGame).ToString() + names);
                        break;
                    case MessageCode.NextPlayer:
                        Int32 next = Convert.ToInt32(message.Substring(1));
                        if (next < PlayerCount)
                            next++;
                        else if (next == PlayerCount)
                            next = 0;
                        SendMessageAll(((Int32)MessageCode.NextPlayer).ToString() + next.ToString());
                        break;
                    case MessageCode.Step:
                        Int32 fields = Int32.Parse(message.Substring(1));
                        SendMessageAll(((Int32)MessageCode.Step).ToString() + fields.ToString());
                        break;
                }

                _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceive_Completed), _ClientSocket);

            }
            catch (Exception ex)
            {
                if (_ClientSocket != null)
                {
                    Console.WriteLine("{0} kapcsolata lezárva.", PlayerName);
                    _ClientSocket.Close();
                    _ClientSocket = null;
                }
            }
        }

        private void SendMessageAll(String message)
        {
            foreach (GameClient client in _Server.Clients.Values)
            {
                client.SendMessage(message);
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
