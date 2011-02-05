using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Xml;

namespace IDMEFParser
{
    public class IDMEF
    {
        private string file;
        private string host;
        private string port;
        private string databaseName;
        private string username;
        private string password;

        public string MessageID = "";
        public string AnalyzerID = "";
        public string CreateTime = "";
        public string DetectTime = "";
        public string SourceNetworkAddress = "";
        public string SourcePort = "";
        public string TargetNetworkAddress = "";
        public string TargetPort = "";
        public string AssessmentImpact = "";

        public bool IsParsed = false;

        public IDMEF(string fileName)
        {
            file = fileName;
        }

        public IDMEF(string inputFile, string inputHost, string inputPort, string inputDatabaseName, string inputUsername, string inputPassword)
        {
            file = inputFile;
            host = inputHost;
            port = inputPort;
            databaseName = inputDatabaseName;
            username = inputUsername;
            password = inputPassword;
        }

        public void SetHost(string inputHost)
        {
            host = inputHost;
        }
        public void SetPort(string inputPort)
        {
            port = inputPort;
        }
        public void SetDatabaseName(string inputDatabaseName)
        {
            databaseName = inputDatabaseName;
        }
        public void SetUsername(string inputUsername)
        {
            username = inputUsername;
        }
        public void SetPassword(string inputPassword)
        {
            password = inputPassword;
        }

        public void Parse()
        {
            XmlTextReader reader = new XmlTextReader(file);
            reader.WhitespaceHandling = WhitespaceHandling.None;

            bool isSource = true;
            //bool isIPAddress = false;
            //bool isPort = false;
            int textType = 0;
            //1 CreateTime
            //2 IPAddress
            //3 Port

            try
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "idmef:Alert":
                                    if (reader.HasAttributes)
                                    {
                                        reader.MoveToNextAttribute();
                                        MessageID = reader.Value;
                                        //MessageBox.Show("MessageID " + MessageID);
                                    }
                                    break;
                                case "idmef:Analyzer":
                                    if (reader.HasAttributes)
                                    {
                                        reader.MoveToNextAttribute();
                                        AnalyzerID = reader.Value;
                                        //MessageBox.Show("AnalyzerID " + AnalyzerID);
                                    }
                                    break;
                                case "idmef:CreateTime":
                                    textType = 1;                                    
                                    break;
                                case "idmef:Source":
                                    isSource = true;
                                    break;
                                case "idmef:Target":
                                    isSource = false;
                                    break;
                                case "idmef:address":
                                    textType = 2;
                                    break;
                                case "idmef:port":
                                    textType = 3;
                                    break;
                                case "idmef:Impact":
                                    if (reader.HasAttributes)
                                    {
                                        reader.MoveToNextAttribute();
                                        AssessmentImpact = reader.Value;
                                        //MessageBox.Show("Impact " + AssessmentImpact);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            switch (textType)
                            {
                                case 1:
                                    CreateTime = reader.Value;
                                    textType = 0;
                                    //MessageBox.Show("CreateTime " + CreateTime);
                                    break;                                    
                                case 2:                                                                                   
                                    if (isSource == true)
                                    {
                                        SourceNetworkAddress = reader.Value;
                                        //MessageBox.Show("SourceAdd " + SourceNetworkAddress);
                                    }
                                    else
                                    {
                                        TargetNetworkAddress = reader.Value;
                                        //MessageBox.Show("TargetAdd " + TargetNetworkAddress);
                                    }
                                    textType = 0;
                                    break;
                                case 3:
                                    if (isSource == true)
                                    {
                                        SourcePort = reader.Value;
                                        //MessageBox.Show("SourcePort " + SourcePort);
                                    }
                                    else
                                    {
                                        TargetPort = reader.Value;
                                        //MessageBox.Show("TargetPort " + TargetPort);
                                    }
                                    textType = 0;
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case XmlNodeType.EndElement:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            IsParsed = true;
            MessageBox.Show("Alert is parsed successfully");
        }

        public void addToDatabase()
        {
            if (IsParsed == false)
                return;
            OracleConnection conn = null;
            try
            {
                conn = DatabaseConnection.getDatabaseConnection(host, port, databaseName, username, password);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                if (SourcePort == "")
                    SourcePort = "''";
                if (TargetPort == "")
                    TargetPort = "''";

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

    }
}
