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

namespace SecVizAdminApp
{
    public delegate void ClickOnNodeDelegate(string hyperAlertType, string hyperAlertName, DateTime beginTime, DateTime endTime);
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node()
        {
            InitializeComponent();
            hyperAlertNode.Width = DEFAULT_WIDTH;
            hyperAlertNode.Height = DEFAULT_WIDTH;
            hyperAlertNode.Fill = NODE_COLOR;

            //hyperAlertName_textBlock.Text = haName;
            hyperAlertName_textBlock.Width = DEFAULT_WIDTH;
            hyperAlertName_textBlock.FontSize = FONT_SIZE;
        }
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

        }

        public Node(string haName, string haType)
        {
            
            InitializeComponent();
            this.HyperAlertName = haName;
            this.HyperAlertType = haType;
            

            hyperAlertNode.Width = DEFAULT_WIDTH;
            hyperAlertNode.Height = DEFAULT_WIDTH;
            hyperAlertNode.Fill = NODE_COLOR;

            hyperAlertName_textBlock.Text = haName;
            hyperAlertName_textBlock.Width = DEFAULT_WIDTH;
            hyperAlertName_textBlock.FontSize = FONT_SIZE;
        }

        public string Name;

        public void SetName(string n)
        {
            hyperAlertName_textBlock.Text = n;
            Name = n;
        }
       
        public string HyperAlertType;
        public string HyperAlertName;
        public DateTime BeginTime;
        public DateTime EndTime;

        public ClickOnNodeDelegate NodeDelegate;

        public void SetPosition(int row, int col)
        {
            this.Row = row;
            this.Column = col;
        }

        public int Row;
        public int Column;

        private void HyperAlertNode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hyperAlertNode.Stroke = STROKE_COLOR;
            hyperAlertNode.StrokeThickness = 3;
            this.NodeDelegate(HyperAlertType, HyperAlertName, BeginTime, EndTime);
        }

        const double FONT_SIZE = 12;
        const int DEFAULT_WIDTH = 30;
        public const int DEFAULT_INDEX = -1;
        public const int MAX_INDEX = 99999;
        Brush NODE_COLOR = Brushes.LightBlue;
        Brush STROKE_COLOR = Brushes.Red;
    }
}
