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
using GazdalkodjOkosan.Model.Game;
using GazdalkodjOkosan.Model.Actions;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for PurchaseDialog.xaml
    /// </summary>
    public partial class PurchaseDialog : Window
    {
        private String[] items;
        private List<CheckBox> boxes;

        public PurchaseDialog(IAction action)
        {
            items = action.Message.Split(new string[]{"Ft\n"},StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();
            InitializeLabels();
        }

        private void InitializeLabels()
        {
            boxes = new List<CheckBox>();

            int cnt = 1;
            foreach (String item in items)
            {
                string[] parts = item.Split(new char[] { '\t' });
                Label lab = new Label();
                lab.Content = parts[0];
                Label lab2 = new Label();
                lab2.Content = parts[1];
                CheckBox box = new CheckBox();

                MainGrid.Children.Add(lab);
                MainGrid.Children.Add(lab2);
                MainGrid.Children.Add(box);
                box.IsChecked = false;
                Canvas.SetLeft(lab, 30);
                Canvas.SetTop(lab, 30 * cnt);
                Canvas.SetLeft(lab2, 120);
                Canvas.SetTop(lab2, 30 * cnt);
                Canvas.SetLeft(box, 10);
                Canvas.SetTop(box, 5+ 30 * cnt++);
                box.Width = 15;
                box.Height = 15;
            }
        }
    }
}
