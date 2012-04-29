using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gazdalkodj_Okosan.Network;
using System.Net.Sockets;
using System.Net;
using Gazdalkodj_Okosan.Model.Network;
using GazdalkodjOkosan.Control;
using System.Threading;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //ClientController Controller;
       // private delegate void ProcessMessageSendDelegate(MessageCode code);

        public MainWindow()
        {
            InitializeComponent();
            //Controller = new ClientController();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Controller.StartServer();
            //Controller.BeginConnect(IPAddress.Parse("192.168.56.1"), "Startjátékos");
            //Thread.Sleep(1000);
            //Controller.NewGame();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //Controller.BeginConnect(IPAddress.Parse("192.168.56.1"), "Szandi");
            //Thread.Sleep(1000);
            //Controller.ConnectToGame();
        }
    }
}
