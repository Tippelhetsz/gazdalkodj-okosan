using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using GazdalkodjOkosan.Model.Game;
using Gazdalkodj_Okosan.Model.Network;

namespace Gazdalkodj_Okosan.Network
{
    /// <summary>
    /// Játékszerver típusa.
    /// </summary>
    public class GameServer
    {
        private Socket _Listener;
        private Int32 _ServerPort;
        private Boolean _RunServer;
        private Int32 _ClientCounter;

        /// <summary>
        /// Játékkliens objektumok lekrédezése.
        /// </summary>
        public Dictionary<Int32, GameClient> Clients { get; private set; }

        /// <summary>
        /// Játékosok adatainak lekérdezése.
        /// </summary>
        public List<GameData> GameData
        {
            get
            {
                List<GameData> gameData = new List<GameData>();
                foreach (GameClient client in Clients.Values)
                {
                    if (client.State == ClientState.CreatedNewGame)
                        gameData.Add(client.gameData);
                }
                return gameData;
            }
        }

        public GameServer()
        {
            Clients = new Dictionary<Int32, GameClient>();
            _ServerPort = 4350;
            _ClientCounter = 0;
        }

        public void Start()
        {
            Thread serverThread = new Thread(WatchConnections);
            serverThread.Start();

            Thread watchThread = new Thread(WatchServer);
            watchThread.Start();
        }
        /// <summary>
        /// Szerver leállítása.
        /// </summary>
        public void Stop()
        {
            _RunServer = false;
            if (_Listener != null)
                _Listener.Close();
        }

        /// <summary>
        /// Kapcsolatok fogadása.
        /// </summary>
        private void WatchConnections()
        {
            try
            {
                String serverName = Dns.GetHostName(); // a szerver DNS nevének lekérdezése
                List<IPAddress> addressList = new List<IPAddress>();
                IPHostEntry ipEntry = Dns.GetHostEntry(serverName); // a szerver nevének megfelelő címek lekérdezése
                foreach (IPAddress address in ipEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork) // ha ez IPv4 cím
                    {
                        addressList.Add(address); // akkor felvesszük a lehetséges címek közé
                    }
                }

                // ha sikerült valamit kiolvasni:
                if (addressList.Count < 1)
                {
                    Console.WriteLine("Hiba történt: nem sikerült begyűjteni a lokális gép hálózati címét.");
                }
                else
                {
                    Console.WriteLine("Hallgatózás a következő címen: [{0}] {1}:{2}.", serverName, addressList[0], _ServerPort);

                    _RunServer = true;
                    _Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _Listener.Bind(new IPEndPoint(addressList[0], _ServerPort)); // csatolás IP címhez
                    _Listener.Listen(10); // maximum 10 kapcsolat

                    while (_RunServer && (_ClientCounter < 6))
                    {
                        Socket clientSocket = _Listener.Accept();
                        Clients.Add(_ClientCounter, new GameClient(this, _ClientCounter, clientSocket));
                        _ClientCounter++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Szerver felügyelete.
        /// </summary>
        private void WatchServer()
        {
            while (_RunServer)
            {
                Thread.Sleep(1000);
                foreach (Int32 id in Clients.Keys.ToList())
                {
                    if (!Clients[id].Connected) // ha egy kliens már lecsatalkozott
                        Clients.Remove(id); // akkor töröljük
                }
            }
        }
    }
}
