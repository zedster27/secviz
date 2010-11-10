using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.AttackRecognition.DataModel
{
    public class Implication
    {
        private int[] impliedArgIDArray;
        private String impliedName;
        private int[] implyingArgIDArray;
        private String implyingName;

        public int getImpliedArgID(int index)
        {
            return this.impliedArgIDArray[index];
        }

        public int getImplyingArgID(int index)
        {
            return this.implyingArgIDArray[index];
        }

        public Implication()
        { 

        }

        public Implication(String implying, String implied)
        {
            this.impliedName = implied;
            this.implyingName = implying;
        }
    }
}