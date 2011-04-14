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
using SecVizAdminApp.ServerMonitorService;
using System.Threading;
using System.Windows.Threading;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for PredicateView.xaml
    /// </summary>
    public partial class PredicateView : UserControl
    {
        public PredicateView()
        {
            InitializeComponent();

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            //bWorker.RunWorkerAsync();
        }

        public BackgroundWorker bWorker;

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     this.DataContext = new
                     {
                         Preds = predList
                     };
                 });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            predList = new ServerMonitorService.ServerMonitorService().GetKBPredicates();

        }

        KBPredicate[] predList;

        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            bWorker.RunWorkerAsync();
        }
    }
}
