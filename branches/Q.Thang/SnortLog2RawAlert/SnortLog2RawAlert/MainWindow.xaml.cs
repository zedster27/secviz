using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//using System.Windows.
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using Microsoft.Win32;

namespace SnortLog2RawAlert
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
        class DatabaseConnection
        {
            public static OracleConnection getDatabaseConnection(string host, string port, string databaseName, string username, string password)
            {
                string connString = "Data Source=" + host + ":" + port + "/" + databaseName + ";"
                                    + "User Id=" + username + ";" + "Password=" + password + ";";
                OracleConnection conn = new OracleConnection(connString);
                return conn;
            }

            public static OracleConnection getDatabaseConnection(string connString)
            {
                OracleConnection conn = new OracleConnection(connString);
                return conn;
            }

            public static OracleConnection GetDefaultConnection()
            {
                string connString = "Data Source=localhost:1521/orcl;User Id=admin;Password=password;";
                OracleConnection conn = new OracleConnection(connString);
                return conn;
            }

            public static OracleConnection GetCorrelationConnection()
            {
                string connString = "Data Source=localhost:1521/orcl;User Id=admin;Password=password;";
                OracleConnection conn = new OracleConnection(connString);
                return conn;
            }
        }
        public static void InsertDB(string messID,string asseImpact,string time,string sourceIP,string targetIP)
        {
            OracleConnection conn = null;
            string host = "";
            string port = "";
            string databaseName = "";
            string username = "";
            string password = "";

            string MessageID = "''";//
            string SourcePort = "''";//
            string TargetPort = "''";//
            string AnalyzerID = "''";//
            string CreateTime = time;//
            string SourceNetworkAddress = sourceIP;
            string TargetNetworkAddress = targetIP;//
            string AssessmentImpact = asseImpact;//

            

            try
            {
                conn = DatabaseConnection.getDatabaseConnection(host, port, databaseName, username, password);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

            

                string query = "insert into raw_alert(MessageID,AnalyzerID,CreateTime,SourceNetworkAddress,SourcePort,TargetNetworkAddress,TargetPort,AssessmentImpact) " +
                               "values('" + MessageID + "','" + AnalyzerID + "','" + CreateTime + "','" + SourceNetworkAddress + "'," + SourcePort + ",'"
                               + TargetNetworkAddress + "'," + TargetPort + ",'" + AssessmentImpact + "')";
                //MessageBox.Show(query);
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteReader();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Dispose();
                MessageBox.Show("Alert is added to database");
            }
 
        }
        private void Run_button_Click(object sender, RoutedEventArgs e)
        {
           // string textString = File.ReadAllText(@"..\..\test.txt");
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
           string textString = File.ReadAllText(dlg.FileName);
            string[] aa = textString.Split(new string[] {"\n"}, StringSplitOptions.None);
            List<string> alert = new List<string>();
            List<string> names = new List<string>() ;
            foreach (string a in aa)
            {
                if (a.Trim() != "")
                {
                    alert.Add(a);
                   // MessageBox.Show(ele);
                }
                else  
                {
                   
                   // MessageBox.Show("finish 1 alert");
                    if (alert.Count == 0) break;
                    string ele1 = alert.ElementAt(0);
                    //ele1 = ele1.Remove(0, 1);
                    //ele1 = ele1.Replace("[**]", ""); 
                    string[] sub1 = ele1.Split(']');
                    ele1 = sub1[2];
                    sub1 = ele1.Split('[');
                    ele1 = sub1[0];
                    //MessageBox.Show(ele1);
                  
                    bool flag = false;
                    for( int i = 0 ; i< names.Count;i++)
                    {
                        if (ele1 == names.ElementAt(i))
                        {
                            flag = true;
                           // break;
                        }
                    }
                    if (flag != true)
                    { 
                        names.Add(ele1);
                       // MessageBox.Show(ele1);
                    }
                   // MessageBox.Show(ele1);
                    string ele2 = alert.ElementAt(1); 
                    string[] sub2 = ele2.Split(':');
                    ele2 = sub2.Last();
                       
                    ele2 = ele2.Replace("]","");
                   // MessageBox.Show(ele2);
                    string ele3 = alert.ElementAt(2);
                    string[] sub3 = ele3.Split(' ');
                    string time = sub3.First();
                    string sourceIP = sub3.ElementAt(1);
                    string targetIP = sub3.Last();
                   // MessageBox.Show(ele1 + " "+ele2+" "+time+" "+sourceIP+" "+targetIP);
                    InsertDB(ele1, ele2, time, sourceIP, targetIP);
                    alert.Clear();
                }
                
            }
            string[] lines = new string[names.Count];
            
            for( int i = 0 ; i< names.Count;i++)
            {
                lines[i] = names.ElementAt(i) + " , " ;
            }
            System.IO.File.WriteAllLines(@"C:\Users\Thang Le\Documents\Visual Studio 2010\Projects\SnortLog2RawAlert\Names.txt", lines);

            MessageBox.Show("DONE");
            /*string el1 = alert.ElementAt(0);
            el1 = el1.Remove(0, 15);
            el1 = el1.Replace("[**]", "");
           // MessageBox.Show(el1);
            string el2 = alert.ElementAt(1);
            string el3 = alert.ElementAt(2);*/
               
        }
    }
}
