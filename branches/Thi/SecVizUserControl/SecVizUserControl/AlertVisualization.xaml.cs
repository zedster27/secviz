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
    /// Interaction logic for AlertVisualization.xaml
    /// </summary>
    public partial class AlertVisualization : UserControl
    {
        public AlertVisualization()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AlertVisualization_Loaded);

            monitorService = new ServerMonitorService.ServerMonitorService();

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            //bWorker.RunWorkerAsync();

            linesList = new List<Line>();
            textList = new List<TextBox>();
        }

        void AlertVisualization_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }


        private void clearCanvas()
        {
            foreach (var line in linesList)
            {
                canvas1.Children.Remove(line);
            }
            foreach (var t in textList)
            {
                canvas1.Children.Remove(t);
            }
        }

        private void drawText(string text, double x, double y)
        {
            TextBox t = new TextBox();
            t.Text = text;
            //t.Background = Color.FromArgb(0, 1, 1, 1);
            canvas1.Children.Add(t);
            Canvas.SetTop(t, y);
            Canvas.SetLeft(t, x);
            textList.Add(t);
        }

        private void drawCanvas()
        {
            if (optionCombobox.SelectedIndex == 0) currentList = portList;
            else currentList = ipList;

            if (currentList == null) return;
            int nPart = currentList.Count;
            if (nPart == 0) return;
            foreach (var alert in rawAlertList)
            {
                if (optionCombobox.SelectedIndex == 0)
                {
                    int sInd = getPortIndex(alert.SourcePort);
                    int tInd = getPortIndex(alert.TargetPort);

                    Line line = new Line();
                    line.X1 = X_START;
                    line.X2 = X_END;
                    line.Y1 = (double)sInd / nPart * DRAW_LENGTH + Y_START;
                    line.Y2 = (double)tInd / nPart * DRAW_LENGTH + Y_START;

                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1;

                    linesList.Add(line);
                    canvas1.Children.Add(line);

                    drawText(alert.SourcePort, X_START, line.Y1);
                    drawText(alert.TargetPort, X_END, line.Y2);
                }
                else
                {
                    int sInd = getIpAddrIndex(alert.SourceNetworkAddress);
                    int tInd = getIpAddrIndex(alert.TargetNetworkAddress);

                    Line line = new Line();
                    line.X1 = X_START;
                    line.X2 = X_END;
                    line.Y1 = (double)sInd / nPart * DRAW_LENGTH + Y_START;
                    line.Y2 = (double)tInd / nPart * DRAW_LENGTH + Y_START;

                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1;

                    linesList.Add(line);
                    canvas1.Children.Add(line);

                    drawText(alert.SourceNetworkAddress, X_START, line.Y1);
                    drawText(alert.TargetNetworkAddress, X_END, line.Y2);
                }
            }
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     clearCanvas();
                     drawCanvas();
                 });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            rawAlertList = monitorService.GetRawAlerts();
            ipList = new List<string>();
            portList = new List<string>();
            findListIp();
            findListPort();
        }

        int getPortIndex(string p)
        {
            for (int i = 0; i < portList.Count; i++)
                if (portList[i] == p) return i;
            return -1;
        }

        int getIpAddrIndex(string ip)
        {
            for (int i = 0; i < ipList.Count; i++)
                if (ipList[i] == ip) return i;
            return -1;
        }

        void findListPort()
        {
            foreach (var al in rawAlertList)
            {
                if (getPortIndex(al.SourcePort) < 0)
                {
                    portList.Add(al.SourcePort);
                }
                if (getPortIndex(al.TargetPort) < 0)
                {
                    portList.Add(al.TargetPort);
                }
            }
        }
        void findListIp()
        {
            foreach (var al in rawAlertList)
            {
                if (getPortIndex(al.SourceNetworkAddress) < 0)
                {
                    ipList.Add(al.SourceNetworkAddress);
                }
                if (getPortIndex(al.TargetNetworkAddress) < 0)
                {
                    ipList.Add(al.TargetNetworkAddress);
                }
            }
        }

        private void option_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     clearCanvas();
                     drawCanvas();
                 });
        }

        private BackgroundWorker bWorker;
        RawAlert[] rawAlertList;
        List<string> ipList;
        List<string> portList;
        List<string> currentList;

        List<Line> linesList;
        List<TextBox> textList;
        private ServerMonitorService.ServerMonitorService monitorService;

        const int X_START = 10;
        const int X_END = 500;
        const int Y_START = 30;
        const int DRAW_LENGTH = 400;
    }
}
