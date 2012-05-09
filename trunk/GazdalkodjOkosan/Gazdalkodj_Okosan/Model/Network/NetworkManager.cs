using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gazdalkodj_Okosan.Network;
using System.Net.Sockets;
using System.Net;
using GazdalkodjOkosan.Control;
using GazdalkodjOkosan.Model.Game;

namespace Gazdalkodj_Okosan.Model.Network
{
    public class NetworkManager
    {
        private GameServer server;
        private ClientController _Controller;
        private Socket _ClientSocket;
        private MessageCode _CurrentCode; // aktuális üzenet kódja
        public ClientState State;
        private String PlayerName;
        //private Int32 PlayerId;
        //public event EventHandler StateChanged; //eseménykezelő az állapotok változására
        private Byte[] _Buffer;
        private delegate void ProcessMessageReceiveDelegate(String message);

        public void StartServer()
        {
            server = new GameServer();
            server.Start();
        }

        public void BeginConnect(IPAddress address, String playername)
        {
            PlayerName = playername;
            _Buffer = new Byte[1024];
            _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ClientSocket.BeginConnect(address, 4350, new AsyncCallback(ConnectCallback), _ClientSocket);
        }

        public void StartGame()
        {
            Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.StartGame).ToString());
            _CurrentCode = MessageCode.StartGame;
            _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSendCallback), _ClientSocket);
        }

        public void NextPlayer(Int32 nextPlayer)
        {
            Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.NextPlayer).ToString() + nextPlayer);
            _CurrentCode = MessageCode.NextPlayer;
            _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSendCallback), _ClientSocket);
        }

        public void Step(Int32 roll)
        {
            Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.Step).ToString() + roll);
            _CurrentCode = MessageCode.Step;
            _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSendCallback), _ClientSocket);
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                _ClientSocket.EndConnect(asyncResult);

                Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.ConnectToServer).ToString() + PlayerName);
                _CurrentCode = MessageCode.ConnectToServer;
                _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSendCallback), _ClientSocket);
            }
            catch (SocketException)
            {
                Console.WriteLine("Megszakadt a kapcsolat a szerverrel!");
            }
            catch (NullReferenceException) { }
            catch (ObjectDisposedException) { }
        }

        private void MessageSendCallback(IAsyncResult asyncResult)
        {
            SocketError error;
            _ClientSocket.EndSend(asyncResult, out error);

            if (error == SocketError.Success)
            {
                ProcessMessageSend(_CurrentCode);
            }
            else
                Console.WriteLine("Nem sikerült adatokat küldeni a szervernek!");
        }

        public void ProcessMessageSend(MessageCode code)
        {
            switch (code)
            {
                case MessageCode.ConnectToServer:
                    Console.WriteLine("ConnectToServer...");
                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    break;
                case MessageCode.StartGame:
                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    Console.WriteLine("Játék indítása...");
                    break;
                case MessageCode.NextPlayer:
                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    Console.WriteLine("Következő játékos...");
                    break;
                case MessageCode.Step:
                     _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    Console.WriteLine("Lépés...");
                    break;
            }
        }

        private void MessageReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                SocketError error;
                Int32 byteCount = _ClientSocket.EndReceive(asyncResult, out error);

                if (error == SocketError.Success)
                {
                    String message = Encoding.UTF8.GetString(_Buffer, 0, byteCount);
                    ProcessMessageReceive(message);

                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                }
                else
                {
                    Console.WriteLine("Megszakadt a kapcsolat a szerverrel!");
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Megszakadt a kapcsolat a szerverrel!");
            }
            catch (NullReferenceException) { }
            catch (ObjectDisposedException) { }
        }

        private void ProcessMessageReceive(String message)
        {
            //Byte[] dataBytes;
            try
            {
                switch ((MessageCode)Int32.Parse(message.Substring(0, 1)))
                {
                    case MessageCode.Connected:
                        PlayerName = message.Substring(1);
                        Console.WriteLine("Kedves" + PlayerName + ", Ön sikeresen csatlakozott a szerverhez.");
                        break;
                    case MessageCode.ConnectToServer:
                        string playername;
                        playername = message.Substring(1);
                        Console.WriteLine(playername + "csatlakozott a szerverhez.");
                        break;
                    case MessageCode.StartGame:
                        Player player;
                        String[] names = message.Substring(1).Split('|');
                        Player[] Players = new Player[names.Length - 1];
                        for (int i = 0; i < names.Length - 1; i++)
                        {
                            player = new Player(names[i]);
                            Players[i] = player;
                        }
                        _Controller.CreateGame(Players);
                        break;
                    case MessageCode.NextPlayer:
                        int nextPlayer = Convert.ToInt32(message.Substring(1));
                        _Controller.NextPlayer(nextPlayer);
                        break;
                    case MessageCode.Step:
                        int fields = Convert.ToInt32(message.Substring(1));
                        _Controller.Step(fields);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
