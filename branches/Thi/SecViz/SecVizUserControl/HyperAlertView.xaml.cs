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
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using SecVizAdminApp.ServerMonitorService;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for HyperAlertView.xaml
    /// </summary>
    public partial class HyperAlertView : UserControl
    {
        public HyperAlertView()
        {
            InitializeComponent();
            ImageBrush brush = new ImageBrush();
            Uri uri = new Uri(Constant.BUTTON_BACKGROUND_PATH);
            BitmapImage img = new BitmapImage(uri);
            brush.ImageSource = img;
            reloadButton.Background = brush;
            viewdetailButton.Background = brush;
            addButton.Background = brush;

            ImageBrush brushBackground = new ImageBrush();
            Uri uriBackground = new Uri(Constant.BACKGROUND_PATH);
            BitmapImage imgBackground = new BitmapImage(uriBackground);
            brushBackground.ImageSource = imgBackground;
            this.Background = brushBackground;

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            //bWorker.RunWorkerAsync();
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     this.DataContext = new
                     {
                         HyperAlerts = hatNames
                     };
                     hatListView.SelectedIndex = 0;
                 });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            hats = new ServerMonitorService.ServerMonitorService().GetKBHyperAlertTypes();
            hatNames = new List<string>();
            foreach (var hat in hats)
            {
                hatNames.Add(hat.Name);
            }            
        }
        

        private void viewdetailButton_Click(object sender, RoutedEventArgs e)
        {
            int ind = hatListView.SelectedIndex;
            KBHyperAlertType alertType = hats.ElementAt(ind);

            string data = "Facts: \n";
            foreach (var fact in alertType.Facts)
            {
                data += String.Format("\t{0}\n", fact);
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

            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    detailTextblock.Text = data;
                });
        }
        
        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            //bWorker = new BackgroundWorker();
            bWorker.RunWorkerAsync();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        List<string> hatNames;
        KBHyperAlertType[] hats;
        private BackgroundWorker bWorker;
    }
}
