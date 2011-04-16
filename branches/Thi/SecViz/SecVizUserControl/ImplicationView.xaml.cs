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
using SecVizAdminApp.ServerMonitorService;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for ImplicationView.xaml
    /// </summary>
    public partial class ImplicationView : UserControl
    {
        public ImplicationView()
        {
            InitializeComponent();
            ImageBrush brush = new ImageBrush();
            Uri uri = new Uri(Constant.BUTTON_BACKGROUND_PATH);
            BitmapImage img = new BitmapImage(uri);
            brush.ImageSource = img;
            reloadButton.Background = brush;
            
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

        public BackgroundWorker bWorker;

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     this.DataContext = new
                     {
                         Implications = implicationList
                     };
                 });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            implicationList = new ServerMonitorService.ServerMonitorService().GetKBImplications();

        }

        KBImplication[] implicationList;

        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            bWorker.RunWorkerAsync();
        }
    }
}
