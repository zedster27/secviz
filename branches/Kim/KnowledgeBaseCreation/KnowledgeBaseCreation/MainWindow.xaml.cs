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
using System.Xml;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace KnowledgeBaseCreation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbHost.Text = "localhost";           
        }

        KnowledgeBaseCreator creator = null;

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
            tbDataPath.Text  = str;
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            if (creator == null)
                creator = new KnowledgeBaseCreator();
            creator.SetDatabaseName(tbDatabaseName.Text);
            creator.SetHost(tbHost.Text);
            creator.SetPassword(tbPassword.Text);
            creator.SetPort(tbPort.Text);
            creator.SetUsername(tbUserName.Text);
            creator.SetKnowledgeBaseFile(tbDataPath.Text);
            creator.AddKnowledgeBase();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            if (creator == null)
                creator = new KnowledgeBaseCreator();
            creator.SetDatabaseName(tbDatabaseName.Text);
            creator.SetHost(tbHost.Text);      
            creator.SetPassword(tbPassword.Text);
            creator.SetPort(tbPort.Text);
            creator.SetUsername(tbUserName.Text);

            creator.CreateTable();
        }
    }
}
