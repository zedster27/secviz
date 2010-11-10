using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
namespace ServerService.DataModel

{
    public class HyperAlertType
    {
        private PredicateNode[] prerequisiteArray;
        private PredicateNode[] consequenccArray;
        private string name;
        private string protocol;
        private Fact[] facts;
        public HyperAlertType(string newName)
        {
            name = newName;
            facts = getFactFromDB();
            consequenccArray = getConseqFromDB();
            prerequisiteArray = getPrereqFromDB();
        }
        private Fact[] getFactFromDB()
        {
            string oradb = "Data Source=ORCL;User Id=hr;Password=hr;";
            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();

            string cmdStr = "select * from HATFact where HyperAlertType=" + name;
            OracleCommand cmd = new OracleCommand(cmdStr, conn);

            OracleDataReader reader = cmd.ExecuteReader();

            List<Fact> retVal = new List<Fact>();
            while (reader.Read())
            {
                string tempName;
                string tempType;
                tempName=reader.GetString(1);
                tempType=reader.GetString(2);
                Fact temp = new Fact(tempName,tempType);
                retVal.Add(temp);
            }
            
            return retVal.ToArray<Fact>();
           
        }

        private PredicateNode[] getPrereqFromDB()
        {
            string oradb = "Data Source=ORCL;User Id=hr;Password=hr;";
            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();

            string cmdStr = "select * from HATPrereq where HyperAlertType=" + name;
            OracleCommand cmd = new OracleCommand(cmdStr, conn);

            OracleDataReader reader = cmd.ExecuteReader();

            return null;
           
        }
        private PredicateNode[] getConseqFromDB()
        {
            return null;
        }

        public Fact[] getFacts()
        {
            return facts;
        }
        public Fact getFact(int index)
        {
            return facts[index];
        }
        PredicateNode getPrerequisiteAtIndex(int i)
        {
            return prerequisiteArray[i];
        }

        PredicateNode[] getPrerequisiteSet()
        {
            return prerequisiteArray;
        }

        PredicateNode getConsequenceAtIndex(int i)
        {
            return consequenccArray[i];
        }

        PredicateNode[] getConsequenceSet()
        {
            return prerequisiteArray;
        }

    }
}