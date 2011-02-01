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

namespace IDMEFParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string browseFile()
        {
            //Console.WriteLine("button browse is clicked");
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.FileName = ""; // default filename
            dialog.InitialDirectory = @".";
            dialog.Title = "Browse Knowledge Base File";
            dialog.DefaultExt = "xml"; // default file extension
            dialog.Filter = "files (*.xml)|*.xml| All files (*.*)|*.*";
            dialog.RestoreDirectory = true;

            dialog.ShowDialog();
            return dialog.FileName;
        }

        private void btLoadDataBrowse_Click(object sender, RoutedEventArgs e)
        {
            string str = null;
            str = browseFile();
            if (str == null)
                return;
            textboxDataPath.Text = str;
        }

        private void buttonParse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}
