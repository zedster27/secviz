using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecVizUserControl
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node()
        {
            InitializeComponent();                 
        }

        public Node(double width, double height, string hyperAlertType, string hyperAlertName, DateTime beginTime, DateTime endTime)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            this.HyperAlertType = hyperAlertType;
            this.HyperAlertName = hyperAlertName;
            hyperAlertName_textBlock.Text = hyperAlertName;
            hyperAlertName_textBlock.Width = width;
            this.BeginTime = beginTime;
            this.EndTime = endTime;
        }     
        public string HyperAlertType;
        public string HyperAlertName;
        public DateTime BeginTime;
        public DateTime EndTime;
    }
}
