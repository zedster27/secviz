using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerService.Utils
{
    public class UniqueID
    {
        static int hyperAlertID = 0;
        static int analysisID = 0;
        static int collectionID = 0;
        static int treeNodeID = 0;

        public UniqueID()
        {
        }

        public static int getNextHyperAlertID()
        {
            return ++hyperAlertID;
        }

        public static int getNextAnalysisID()
        {
            return ++analysisID;
        }

        public static int getNextCollectionID()
        {
            return ++collectionID;
        }

        public static int getNextTreeNodeID()
        {
            return ++treeNodeID;
        }

        public static void setHyperAlertID(int ID)
        {
            hyperAlertID = ID;
        }

        public static void setAnalysisID(int ID)
        {
            analysisID = ID;
        }

        public static void setCollectionID(int ID)
        {
            collectionID = ID;
        }

        public static void setTreeNodeID(int ID)
        {
            treeNodeID = ID;
        }
        
    }
}