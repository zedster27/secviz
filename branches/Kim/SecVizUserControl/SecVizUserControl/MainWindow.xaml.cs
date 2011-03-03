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
            item.AddButton("A.1");
            item.AddButton("A.2");
            item.AddButton("A.3");
            item.IsSelected = true;
            //item.Margin = new Thickness(0, 0, this.Width - item.Width, this.Height - item.Height);


            mainGrid.Children.Add(item);
        }
    }
}
