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
using System.Windows.Shapes;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NameInputDialog : Window
    {
        MainWindow ParentWindow;

        public NameInputDialog(MainWindow parent)
        {
            InitializeComponent();
            ParentWindow = parent;
            textBox1.Focus();
            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
        }

        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                button1_Click(null, null);
        }
            

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                ParentWindow.PlayerName = textBox1.Text;
                this.Close();
            }
        }
    }
}
