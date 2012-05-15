using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using GazdalkodjOkosan.Control;
using GazdalkodjOkosan.Model.Game;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IController Controller;
       // private delegate void ProcessMessageSendDelegate(MessageCode code);
        Gazdalkodj_Okosan.Model.Network.NetworkManager manager;
        private String _PlayerName;

        public String PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
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
            StartNetwork();
            InitServerEngine();
            ShowNameInputDialog();
            ShowConnectionGrid();
        }

        private void StartNetwork()
        { 
            //NetworkManager
        }

        private void InitServerEngine()
        { 
            //GameEngine
        }

        private void InitClientEngine()
        { 
            //ClientController
        }

        private void ShowConnectionGrid()
        {
            MainButtonGrid.Visibility = System.Windows.Visibility.Hidden;
            ConnectionGrid.Visibility = System.Windows.Visibility.Visible;
            UpdateConnectionInfo();
        }

        private void ShowMainMenu()
        {
            MainButtonGrid.Visibility = System.Windows.Visibility.Visible;
            ConnectionGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void NewGameEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NewGameText_MouseDown(sender, e);
        }

        private void ConnectToText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowIpDialog();
        }

        private void ShowNameInputDialog()
        {
            NameInputDialog dialog = new NameInputDialog(this);
            dialog.ShowDialog();
        }

        private void ShowIpDialog()
        {
            IPInputDialog dialog = new IPInputDialog();
            dialog.Closed += new EventHandler(dialog_Closed);
            dialog.Show();
        }

        void dialog_Closed(object sender, EventArgs e)
        {
            IPInputDialog dialog = (IPInputDialog)sender;
            PlayerName = dialog.Name;
           // manager.BeginConnect(dialog.IPAddress, dialog.Name);
            ShowConnectionGrid();
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
            Player[] players = new Player[] { new Player(PlayerName), new Player("A másik") };


            BoardWindow board = new BoardWindow();
            board.Show();
        }

        private void NewGameEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            NewGameEllipse.Fill = NewGameEllipse.Stroke = Brushes.Purple;
        }

        private void ConnectToEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ConnectToEllipse.Fill = ConnectToEllipse.Stroke = Brushes.Purple;
        }

        private void QuitEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            QuitEllipse.Fill = QuitEllipse.Stroke = Brushes.Purple;
        }

        private void NewGameEllipse_MouseLeave(object sender, MouseEventArgs e)
        {
            NewGameEllipse.Fill = NewGameEllipse.Stroke = new SolidColorBrush(Color.FromRgb(215, 0, 0));
        }

        private void ConnectToEllipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ConnectToEllipse.Fill = ConnectToEllipse.Stroke = new SolidColorBrush(Color.FromRgb(215, 0, 0));
        }

        private void QuitEllipse_MouseLeave(object sender, MouseEventArgs e)
        {
            QuitEllipse.Fill = QuitEllipse.Stroke = new SolidColorBrush(Color.FromRgb(215, 0, 0));
        }

        private void NewGameText_MouseEnter(object sender, MouseEventArgs e)
        {
            NewGameEllipse_MouseEnter(sender, e);
        }

        private void ConnectToText_MouseEnter(object sender, MouseEventArgs e)
        {
            ConnectToEllipse_MouseEnter(sender, e);
        }

        private void QuitGameText_MouseEnter(object sender, MouseEventArgs e)
        {
            QuitEllipse_MouseEnter(sender, e);
        }
    }
}
