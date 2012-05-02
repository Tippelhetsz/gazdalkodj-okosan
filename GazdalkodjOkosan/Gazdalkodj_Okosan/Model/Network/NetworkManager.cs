using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gazdalkodj_Okosan.Network;
using System.Net.Sockets;
using System.Net;

namespace Gazdalkodj_Okosan.Model.Network
{
    public class NetworkManager
    {
        private GameServer server;
        private Socket _ClientSocket;
        private MessageCode _CurrentCode; // aktuális üzenet kódja
        public ClientState State;
        private String PlayerName;
        private Int32 PlayerId;
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

        public void NewGame()
        {
            Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.NewGame).ToString());
            _CurrentCode = MessageCode.NewGame;
            _ClientSocket.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, new AsyncCallback(MessageSendCallback), _ClientSocket);
        }

        public void ConnectToGame()
        {
            Byte[] dataBytes = Encoding.UTF8.GetBytes(((Int32)MessageCode.ConnectToGame).ToString() + PlayerId.ToString());
            _CurrentCode = MessageCode.ConnectToGame;
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
                case MessageCode.ConnectToGame:
                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    break;
                case MessageCode.NewGame:
                    _ClientSocket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, new AsyncCallback(MessageReceiveCallback), _ClientSocket);
                    Console.WriteLine("Várakozás játékosra...");
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
                    case MessageCode.ConnectToServer:
                        PlayerId = Int32.Parse(message.Substring(1));
                        Console.WriteLine("Csatlakoztatva a szerverhez");
                        break;
                    case MessageCode.ConnectReceived: // valaki csatlakozott a játékhoz
                        Console.WriteLine(message.Substring(1) + ", ön csatlakozott a játékhoz.");
                        break;
                    //case MessageCode.GameOver: // ellenfél veszített
                    //    String player = message.Substring(1);
                    //    Console.WriteLine(player + " befejezte a játékot! ");
                    //    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
