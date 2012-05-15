using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Net;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for IPInputDialog.xaml
    /// </summary>
    public partial class IPInputDialog : Window
    {
        private IPAddress address;
        public IPAddress IPAddress { get { return address; } }

        private string name;
        public string Name { get { return name; } }

        private Boolean FilledCorrectly = false;

        public IPInputDialog()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(IPInputDialog_Closing);
            textBox1.Focus();
        }

        void IPInputDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!FilledCorrectly)
            {
                e.Cancel = true;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Megvizsgáljuk, hogy lehet-e IP cim
                address = IPAddress.Parse(textBox2.Text);
                if (textBox2.Text.Length > 6)
                {
                    SetCorrectness(true);
                }
            }
            catch (FormatException ex)  // Nem IP cim
            {
                SetCorrectness(false);
            }
        }

        private void SetCorrectness(bool correct)
        {
            FilledCorrectly = correct;
            button1.IsEnabled = FilledCorrectly;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Finish();
        }

        private void Finish()
        {
            name = textBox1.Text;
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Finish();
        }

    }
}
