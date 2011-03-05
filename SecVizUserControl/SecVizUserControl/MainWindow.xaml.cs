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

namespace SecVizUserControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TabItem item = new TabItem("A");
            item.AddItem("A.1");
            item.AddItem("A.2");
            item.AddItem("A.3");
            item.AddItem("A.4");
            item.AddItem("A.5");

            item.BorderBrush = Brushes.Black;

            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.VerticalAlignment = VerticalAlignment.Top;
            mainGrid.Children.Add(item);

            //Node newNode = new Node(200, 100, "abc", "xxx", new DateTime(), new DateTime());
            //mainGrid.Children.Add(newNode);
            


        }
    }
}
