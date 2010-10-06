using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.DataModel
{
    public class Alert
    {
        private int alertID;
        private String alertType;
        private DateTime beginTime;
        private DateTime endTime;
        private int collectionID;

        public Alert()
        {

        }

        public Alert(int in_alertID, String in_alertType, DateTime in_beginTime, DateTime in_endTime, int in_collectionID)
        {            

        }

        public int getAlertID()
        {
            return alertID;
        }

        public void setAlertID(int in_alertID)
        {
            alertID = in_alertID;
        }

        public String getAlertType()
        {
            return alertType;
        }

        public void setAlertType(String in_alertType)
        {
            alertType = in_alertType;
        }

        public String getValue(String attributeName)
        {
            return null;
        }

        public String getValue(int index)
        {
            return null;
        }

        public void setValue(String attributeName, String attributeVal)
        {           

        }

        public void setValue(int index, String attributeVal)
        {
            
        }

        public DateTime getBeginTime()
        {
            return beginTime;
        }

        public void setBeginTime(DateTime in_beginTime)
        {
            beginTime = in_beginTime;
        }

        public DateTime getEndTime()
        {
            return endTime;
        }

        public void setEndTime(DateTime in_endTime)
        {
            endTime = in_endTime;
        }

        public int getCollectionID()
        {
            return collectionID;
        }

        public void setCollectionID(int in_collectionID)
        {
            collectionID = in_collectionID;
        }
    }
}