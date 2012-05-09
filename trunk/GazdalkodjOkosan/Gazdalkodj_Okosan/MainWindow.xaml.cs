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

        ClientController Controller;
       // private delegate void ProcessMessageSendDelegate(MessageCode code);
        private String _PlayerName;

        public String PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

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

        private void NewGameText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Controller.StartServer();
            //Controller.BeginConnect(IPAddress.Parse("192.168.56.1"), "Startjátékos");
            //Thread.Sleep(1000);
            //Controller.NewGame();
            MainButtonGrid.Visibility = System.Windows.Visibility.Hidden;
            NameInputDialog dialog = new NameInputDialog(this);
            dialog.ShowDialog();
            ConnectionGrid.Visibility = System.Windows.Visibility.Visible;
            UpdateConnectionInfo();

        }

        private void NewGameEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NewGameText_MouseDown(sender, e);
        }

        private void ConnectToText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Controller.BeginConnect(IPAddress.Parse("192.168.56.1"), "Szandi");
            //Thread.Sleep(1000);
            //Controller.ConnectToGame();
        }

        private void ConnectToEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ConnectToText_MouseDown(sender, e);
        }

        private void QuitEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.QuitGameText_MouseDown(sender, e);
        }

        private void QuitGameText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void UpdateConnectionInfo()
        {
            for (int i = 0; i < ConnectionGrid.Children.Count; ++i)
            {
                try
                {
                    ((Grid)ConnectionGrid.Children[i]).Visibility = Visibility.Hidden;
                }
                catch { }
            }
            ((Grid)ConnectionGrid.Children[1]).Visibility = Visibility.Visible;
            P1StatusText.Inlines.Add(new Run("1. játékos: " + PlayerName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow board = new BoardWindow();
            board.Show();
        }
    }
}
