using System;
using System.ComponentModel;
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
using SecVizAdminApp.ServerMonitorService;
using System.Threading;
using System.Windows.Threading;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for AlertView.xaml
    /// </summary>
    public partial class AlertView : UserControl
    {
        public AlertView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AlertView_Loaded);

            monitorService = new ServerMonitorService.ServerMonitorService();

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            bWorker.ProgressChanged += new ProgressChangedEventHandler(bWorker_ProgressChanged);
            //bWorker.RunWorkerAsync();
        }

        void AlertView_Loaded(object sender, RoutedEventArgs e)
        {
            //dataLoadingProgressbar.Visibility = Visibility.Hidden;
        }

        void bWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     dataLoadingProgressbar.SmallChange += 0.5;
                 });
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     searchButton.IsEnabled = true;
                     refreshButton.IsEnabled = true;
                     dataLoadingProgressbar.Visibility = Visibility.Hidden;
                     this.DataContext = new { 
                        Alerts = rawAlertList
                     };
                 });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
             rawAlertList = monitorService.GetRawAlerts();
        }

        public void ReloadData()
        {
            monitorService.GetRawAlertsAsync();
        }

        RawAlert[] rawAlertList;
        ServerMonitorService.ServerMonitorService monitorService;
        BackgroundWorker bWorker;

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
