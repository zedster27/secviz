using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.AttackRecognition.DataModel
{
    public class Argument
    {
        private int ID;
        private String Name;
        private String Type;

        public Argument()
        {
            this.ID = 0;
            this.Name = "";
            this.Type = "";
        }

        public Argument(int ID, String Type, String Name)
        {
            this.ID = ID;
            this.Type = Type;
            this.Name = Name;
        }
    }
}