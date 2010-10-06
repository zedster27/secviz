using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.DataModel
{
    public class HyperAlertType
    {
        string[] facts;
        PredicateNode[] prerequisiteArray;
        PredicateNode[] consequenccArray;
        string name;

        void getFactFromDB()
        {}

        void getPrereqFromDB()
        {}

        void getConseqFromDB()
        {}

        void getAllFacts()
        {}

        string getFactAtIndex(int i)
        {
            return "";
        }

        PredicateNode getPrerequisiteAtIndex(int i)
        {
            return null;
        }

        object getPrerequisiteSet()
        {
            return null;
        }

        PredicateNode getConsequenceAtIndex(int i)
        {
            return null;
        }

        object getConsequenceSet()
        {
            return null;
        }

    }
}