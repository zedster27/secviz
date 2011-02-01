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
    public class IDMEFParser
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
        public int SourcePort;
        public string TargetNetworkAddress = "";
        public int TargetPort;
        public string AssessmentImpact = "";

        public bool IsParsed = false;

        public IDMEFParser(string fileName)
        {
            file = fileName;
        }

        public IDMEFParser(string inputFile, string inputHost, string inputPort, string inputDatabaseName, string inputUsername, string inputPassword)
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
            bool isIPAddress = false;
            bool isPort = false;

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
                                    }
                                    break;
                                case "idmef:Analyzer":
                                    if (reader.HasAttributes)
                                    {
                                        reader.MoveToNextAttribute();
                                        AnalyzerID = reader.Value;
                                    }
                                    break;
                                case "idmef:CreateTime":
                                    if (reader.HasAttributes)
                                    {
                                        reader.MoveToNextAttribute();
                                        CreateTime = reader.Value;
                                    }
                                    break;
                                case "idmef:Source":
                                    isSource = true;
                                    break;
                                case "idmef:Target":
                                    isSource = false;
                                    break;
                                case "idmef:address":
                                    isIPAddress = true;
                                    break;
                                case "idmef:port":
                                    isPort = true;
                                    break;
                                case "idmef:Impact":
                                    reader.MoveToNextAttribute();
                                    AssessmentImpact = reader.Value;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            if (isIPAddress == true)
                            {
                                if (isSource == true)
                                    SourceNetworkAddress = reader.Value;
                                else TargetNetworkAddress = reader.Value;
                                isIPAddress = false;
                            }
                            else if (isPort == true)
                            {
                                if (isSource == true)
                                    SourcePort = Convert.ToInt32(reader.Value);
                                else TargetPort = Convert.ToInt32(reader.Value);
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
        }

        public void addToDatabase()
        {
            if (IsParsed == false)
                return;
        }

    }
}
