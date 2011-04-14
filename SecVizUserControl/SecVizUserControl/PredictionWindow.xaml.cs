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
using System.Windows.Shapes;
using SecVizAdminApp.CorrelationService;
using SecVizAdminApp.ServerMonitorService;
using System.Threading;
using System.Windows.Threading;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for PredictionWindow.xaml
    /// </summary>
    public partial class PredictionWindow : Window
    {
        public PredictionWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PredictionWindow_Loaded);
        }

        void PredictionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        KBHyperAlertType[] hats;
        public void LoadData()
        {
            PredictedAlert = new CorrelationService.CorrelationService().PredictNextAlerts();
            hats = new ServerMonitorService.ServerMonitorService().GetHyperAlertType(PredictedAlert);
            this.DataContext = new {
                HyperAlerts = PredictedAlert
            };
            alertnameListview.SelectedIndex = 0;
        }

        SearchWindow searchWindow;
        string[] PredictedAlert;

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            //searchWindow = new SearchWindow();
            //searchWindow.MainWindow = new FindWindowDelegate(FindHandler);

            int ind = alertnameListview.SelectedIndex;
            KBHyperAlertType alertType = hats.ElementAt(ind);

            string data = "Facts: \n";
            foreach (var fact in alertType.Facts)
            {
                data += String.Format("\t{0}\n",fact);
            }

            data += "Prerequisite: \n";

            foreach (var pred in alertType.Prerequisites)
            {
                data += String.Format("\t{0}\n", pred.PredicateName);
            }

            data += "Consequence: \n";

            foreach (var pred in alertType.Consequences)
            {
                data += String.Format("\t{0}\n", pred.PredicateName);
            }

            //detailBlock.Text = data;
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    detailBlock.Text = data;
                });
        }

        private void FindHandler(string pattern)
        { 
            
        }
        public ParentWindowGetFocus ParentWindow;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow();
            this.Close();
        }
    }
    public delegate void ParentWindowGetFocus();
}
