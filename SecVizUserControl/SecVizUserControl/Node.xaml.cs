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
    public delegate void ClickOnNodeDelegate(string hyperAlertType, string hyperAlertName, DateTime beginTime, DateTime endTime);
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node(double width, double height, string hyperAlertType, string hyperAlertName, DateTime beginTime, DateTime endTime)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;

            mainNodeGrid.Width = width;
            mainNodeGrid.Height = height;

            hyperAlertNode.Width = width;
            hyperAlertNode.Height = height;
            hyperAlertNode.Fill = NODE_COLOR;

            this.HyperAlertType = hyperAlertType;
            this.HyperAlertName = hyperAlertName;

            hyperAlertName_textBlock.Text = hyperAlertName;
            hyperAlertName_textBlock.Width = width;
            hyperAlertName_textBlock.FontSize = FONT_SIZE;

            this.BeginTime = beginTime;
            this.EndTime = endTime;
        }     
        public string HyperAlertType;
        public string HyperAlertName;
        public DateTime BeginTime;
        public DateTime EndTime;

        ClickOnNodeDelegate nodeDelegate;

        private void HyperAlertNode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hyperAlertNode.Stroke = STROKE_COLOR;
            hyperAlertNode.StrokeThickness = 3;
            this.nodeDelegate(HyperAlertType, HyperAlertName, BeginTime, EndTime);
        }

        const double FONT_SIZE = 30;
        Brush NODE_COLOR = Brushes.LightBlue;
        Brush STROKE_COLOR = Brushes.Red;
    }
}
