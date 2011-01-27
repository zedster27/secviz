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
            btRun.IsEnabled = false;
        }

        private string file = null;

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

        private void addPredicate()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNodeReader reader = new XmlNodeReader(doc.SelectSingleNode("Predicates"));
            
           
        }
        
        private void btLoadDataBrowse_Click(object sender, RoutedEventArgs e)
        {
            string str = null;
            str = browseFile();
            if (str == null)
                return;
            file = str;
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            if (file == null)
                return;
            string connString = "Data Source=" + tbHost.Text + ":" + tbPort.Text + "/" + tbDatabaseName.Text + ";"
                                + "User Id=" + tbUserName.Text + ";" + "Password=" + tbPassword.Text + ";";
            OracleConnection conn = null;
            try
            {
                conn = new OracleConnection(connString);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            string connString = "Data Source=" + tbHost.Text + ":" + tbPort.Text + "/" + tbDatabaseName.Text + ";"
                                + "User Id=" + tbUserName.Text + ";" + "Password=" + tbPassword.Text + ";";
            OracleConnection conn = null;
            try
            {
                conn = new OracleConnection(connString);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                string query;
                
                query = "create table predicate" +
                               "(" +
                                  "PredicateName varchar(100) unique," +
                                  "ArgId varchar(10) unique," +
                                  "ArgPos varchar(10)," +
                                  "ArgAttr varchar(10)," +
                                  "ArgName varchar(10)," +
                                  "primary key (PredicateName, ArgId)" +
                               ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HyperAlertType" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "primary key (HyperAlertTypeName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Fact" +
                        "(" +
                            "FactName varchar(100)," +
                            "FactType varchar(100)," +
                            "primary key (FactName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Protocol" +
                        "(" +
                            "ProtocolName varchar(100)," +
                            "primary key (ProtocolName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Implication" +
                        "(" +
                        "ImplyingName varchar(100)," +
                        "ImpliedName varchar(100)," +
                        "ImplyingArg varchar(10)," +
                        "ImpliedArg varchar(10)," +
                        "primary key (ImplyingName, ImpliedName, ImplyingArg, ImpliedArg)," +
                        "foreign key (ImplyingName) references Predicate(PredicateName)," +
                        "foreign key (ImpliedName) references Predicate(PredicateName)," +
                        "foreign key (ImplyingArg) references Predicate(ArgId)," +
                        "foreign key (ImpliedArg) references Predicate(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HasFact" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "FactName varchar(100),"+
                            "primary key (HyperAlertTypeName, FactName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName),"+
                            "foreign key (FactName) references Fact(FactName)"+
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HasProtocol" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "ProtocolName varchar(100)," +
                            "primary key (HyperAlertTypeName, ProtocolName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (ProtocolName) references Protocol(ProtocolName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Prerequisite" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "PredicateName varchar(100)," +
                            "ArgID varchar(10)," +
                            "primary key (HyperAlertTypeName, PredicateName, ArgID)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (PredicateName) references Predicate(PredicateName)," +
                            "foreign key (ArgId) references Predicate(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Consequence" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "PredicateName varchar(100)," +
                            "ArgID varchar(10)," +
                            "primary key (HyperAlertTypeName, PredicateName, ArgID)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (PredicateName) references Predicate(PredicateName)," +
                            "foreign key (ArgId) references Predicate(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                MessageBox.Show("All tables are created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }

            
        }
    }
}
