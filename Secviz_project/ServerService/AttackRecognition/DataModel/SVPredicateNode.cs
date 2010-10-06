using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.DataModel
{
    public class PredicateNode
    {
        private string predicateName
        {
            set;
            get;
        }

        private int predicateID
        {
            set;
            get;
        }
        private Array predicateFacts
        {
            set;
            get;
        }

    }
}