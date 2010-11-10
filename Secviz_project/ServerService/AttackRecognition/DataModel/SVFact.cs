using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.DataModel
{
    public class Fact
    {
         private string factName
    {
        get
        {
            return factName;
        }
        set
        {
            factName=value;    
        }
    }
    private string factType
    {
        get
        {
            return factType;
        }
        set
        {
            factType=value;    
        }
    }
    public Fact(String name,String type)
    {
        factName=name;
        factType=type;
    }
    public Fact()
    {
    }
       
    }
}