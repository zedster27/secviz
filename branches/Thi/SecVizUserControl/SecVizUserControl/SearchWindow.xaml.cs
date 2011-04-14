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

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    /// 
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow != null)
            {
                MainWindow(findPatterTextbox.Text);
            }
        }

        public FindWindowDelegate MainWindow { get; set; }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public delegate void FindWindowDelegate(string pattern);
}
