using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ServerService.AttackRecognition.DataModel
{
    /// <summary>
    /// This class is a structure to hold one graph node's information.
    /// </summary>
    /// <property name="Type">the hyper alert type of this node</property>
    /// <property name="AlertID">the hyper alert ID of this node</property>
    /// <property name="PostNodes">a list of graph nodes that follow this node</property>
    public class SVGraphNode
    {
        public string Type { get; set; }
        public string AlertID { get; set; }
        public List<SVGraphNode> PostNodes { get; set; }

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="alertID"></param>
        public SVGraphNode(string type, string alertID)
        {
            this.Type = type;
            this.AlertID = alertID;
            PostNodes = new List<SVGraphNode>();
        }
        /// <summary>
        /// This method is used to add a follow node's instance to this node's instance
        /// </summary>
        /// <param name="node">the following node is added</param>
        public void AddPostNode(SVGraphNode node)
        {
            PostNodes.Add(node);
        }
    }
    /// <summary>
    /// This class is used to retrieve attack graph from database.
    /// Its instance will hold all the graph nodes and relationship among them.
    /// </summary>
    /// <property name="Nodes">a collection of all nodes in graph</property>
    public class SVGraphResult
    {
        private HashSet<SVGraphNode> nodes;
        public HashSet<SVGraphNode> Nodes { get { return nodes; } }

        /// <summary>
        /// Graph constructor.
        /// This class can not be instantiated from outside of it.
        /// </summary>
        /// <permission cref="System.Security.PermissionSet">Private</permission>
        private SVGraphResult()
        {
            nodes = new HashSet<SVGraphNode>();
        }

        /// <summary>
        /// this method is used to retrieve attack graph from DB and then flood fill necessary data
        /// to this class.
        /// </summary>
        /// <param name="constraint"></param>
        private void extractGraph(string constraint = "")
        {
            nodes.Clear();
            //retrieve all
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="constraint"></param>
        public static void SaveToFile(string fileName, string constraint)
        { 
            XElement root = new XElement("Graph");
            SVGraphResult graph = new SVGraphResult();
            graph.extractGraph();
            var graphNodes = graph.Nodes;
            foreach (var node in graphNodes)
            {
                XElement alert = new XElement("Node",  
                    new XAttribute("Type", node.Type), new XAttribute("AlertID",node.AlertID));
                foreach (var pNode in node.PostNodes)
                {
                    XElement pAlert = new XElement("ConsequentNode",
                    new XAttribute("Type", pNode.Type), new XAttribute("AlertID", pNode.AlertID));
                    alert.Add(pAlert);
                }
                root.Add(node);
            }
            root.Save(fileName);
        }
    }
}