using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

namespace ServerService.DataModel
{
    public class Alert
    {
        private int sensorID { get; set; }
        private int ID { get; set; }
        private String alertName { get; set; }
        private DateTime beginTime { get; set; }
        private DateTime endTime { get; set; }
        private String srcIpAddr { get; set; }
        private String destIpAddr { get; set; }
        private int srcPort { get; set; }
        private int destPort { get; set; }
        
        public Alert()
        {

        }

        public List<Alert> getAlertFromDestination(String ip, int port)
        {
            return null;
        }

        public List<Alert> getAlertFromSensor(int sensorID)
        {
            return null;
        }

        public List<Alert> getAlertFromSource(String ipAddr, int port)
        {
            return null;
        }

        public List<Alert> getAlertWithInterval(DateTime beginTime, DateTime endTime)
        {
            return null;
        }

        public Alert getNextAlert()
        {
            return null;
        }

        private List<Alert> loadAlertFromDB()
        {
            string oradb = "Data Source=ORCL;User Id=hr;Password=hr;";
            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();

            string cmdStr = "select * from alert_info";
            OracleCommand cmd = new OracleCommand(cmdStr,conn);

            OracleDataReader reader = cmd.ExecuteReader();

            List<Alert> retVal = new List<Alert>();
            while (reader.Read())
            {
                Alert temp = new Alert();
                temp.sensorID = reader.GetInt16(0);
                temp.ID = reader.GetInt16(1);
                temp.alertName = reader.GetString(2);
                temp.beginTime = reader.GetDateTime(3);
                temp.endTime = reader.GetDateTime(4);
                if (!reader.IsDBNull(5))
                {
                    temp.srcIpAddr = reader.GetString(5);
                }
                if (!reader.IsDBNull(6))
                {
                    temp.destIpAddr = reader.GetString(6);
                }
                if (!reader.IsDBNull(7))
                {
                    temp.srcPort = reader.GetInt16(7);
                }
                if (!reader.IsDBNull(8))
                {
                    temp.destPort = reader.GetInt16(8);
                }
                retVal.Add(temp);
            }

            return retVal;
        }
    }
}