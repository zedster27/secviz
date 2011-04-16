using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecVizAdminApp
{
    public class GraphNodeData
    {
        public List<GraphNodeData> FollowNodes { get { return followNodes; } }
        public List<GraphNodeData> PreNodes { get { return preNodes; } }
        private List<GraphNodeData> followNodes;
        private List<GraphNodeData> preNodes;

        public double X;
        public double Y;
        public int Row;
        public int Column;
        public string Name;
        public DateTime BeginTime;
        public DateTime EndTime;

        public GraphNodeData(string haName)
        {
            
            this.Name = haName;


            BeginTime = DateTime.Now;
            EndTime = DateTime.Now;

            followNodes = new List<GraphNodeData>();
            preNodes = new List<GraphNodeData>();
            this.Index = DEFAULT_INDEX;
        }

        

        public void AddFollowNode(GraphNodeData node)
        {
            followNodes.Add(node);
        }

        public void AddPreNode(GraphNodeData node)
        {
            preNodes.Add(node);
        }

        public bool Equals(GraphNodeData node)
        {
            if (node.Name == this.Name)
                return true;
            return false;
        }

        public bool ContainsFollowNode(GraphNodeData node)
        {
            foreach (var tmp in followNodes)
            {
                if (tmp.Equals(node)) return true;
            }
            return false;
        }

        public bool ContainsPreNode(Node node)
        {
            foreach (var tmp in preNodes)
            {
                if (tmp.Equals(node)) return true;
            }
            return false;
        }

        public int Index { get; set; }

        public int MaxPreNodeIndex()
        {
            int rs = DEFAULT_INDEX;
            foreach (var n in preNodes)
            {
                if (rs < n.Index) rs = n.Index;
            }
            return rs;
        }

        public int MinFollowNodeIndex()
        {
            int rs = MAX_INDEX;
            foreach (var n in followNodes)
            {
                if (rs > n.Index) rs = n.Index;
            }
            return rs;
        }

        public void UpdateNodeIndex()
        {
            foreach (var node in followNodes)
            {
                if (node.Index <= this.Index)
                {
                    node.Index = this.Index + 1;
                    node.UpdateNodeIndex();
                }
            }
        }

        public void SetPosition(double x, double y)
        {
            this.X = x;
            this.Y = y;
            //uicontainer.Children.Add(this);
        }

        public void SetPosition(int row, int col)
        {
            this.Row = row;
            this.Column = col;
        }

        const double FONT_SIZE = 30;
        const int DEFAULT_WIDTH = 30;
        public const int DEFAULT_INDEX = -1;
        public const int MAX_INDEX = 99999;
    }
}
