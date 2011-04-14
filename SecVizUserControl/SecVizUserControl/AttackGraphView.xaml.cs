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
using System.IO;
using System.ComponentModel;
using Petzold.Media2D;
using System.Windows.Threading;
using System.Threading;
using SecVizAdminApp.CorrelationService;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for AttackGraphView.xaml
    /// </summary>
    public partial class AttackGraphView : UserControl
    {
        public AttackGraphView()
        {
            InitializeComponent();
            attackNodes = new List<GraphNodeData>();
            this.Loaded += new RoutedEventHandler(AttackGraphView_Loaded);

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
         //   bWorker.RunWorkerAsync();
        }

        void AttackGraphView_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("attack graph view loaded");
        }

        private StringReader dataReader;

        private void loadDataFromServer()
        {
            try
            {
                //TODO: change to get server data
                CorrelationService.CorrelationService service = new CorrelationService.CorrelationService();
                byte[] dotLanguageDataBytes = service.GetFullAttackGraphDotFile();

                string dotFilePath = @"C:\AttackGraph.dot";
                string imgFilePath = @"C:\AttackGraph.png";

                // write received data to file for future use
                MemoryStream memoryStream = new MemoryStream(dotLanguageDataBytes);
                FileStream fileStream = new FileStream(dotFilePath, FileMode.Create);
                memoryStream.WriteTo(fileStream);

                fileStream.Close();
                string dotlanguageDataString;
                using ( StreamReader dataStreamReader = new StreamReader(memoryStream))
                {
                    dotlanguageDataString = dataStreamReader.ReadToEnd();
                }
                 
                Console.WriteLine(dotlanguageDataString);
                memoryStream.Close();

                //generate attack graph image and store to file
                Graphviz.GetImage(dotlanguageDataString, imgFilePath);

                
                this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    Image i = new Image();
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(imgFilePath, UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();
                    i.Source = src;
                    i.Stretch = Stretch.Uniform;
                    this.stackpanel.Children.Clear();
                    this.stackpanel.Children.Add(i);
                });
                

                //TODO: change to get server data
                string attackDataString = service.GetFullAttackGraph();
                dataReader = new StringReader(attackDataString);
                Console.WriteLine("Data loading is finish");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        List<GraphNodeData> attackNodes;

        private GraphNodeData getNode(string name)
        {
            foreach (var node in attackNodes)
            {
                if (node.Name == name) return node;
            }
            GraphNodeData tmp = new GraphNodeData(name);
            attackNodes.Add(tmp);
            return tmp;
        }

        private List<GraphNodeData>[] nodesGrid;
        private int numOfCols;
        private int numOfRows;

        private void initDataToView()
        {
            string line;
            string[] splitStr = {"->", "-->", ",", "---"};
            while (dataReader != null)
            {
                line = dataReader.ReadLine();
                if (line == null) break;
                string[] parts = line.Split(splitStr,StringSplitOptions.None);
                GraphNodeData preNode = this.getNode(parts[0]);
                GraphNodeData followNode = this.getNode(parts[1]);
                preNode.AddFollowNode(followNode);
                followNode.AddPreNode(preNode);
                followNode.Index = preNode.Index + 1;
                followNode.UpdateNodeIndex();
            }


            int maxIndex = GraphNodeData.DEFAULT_INDEX;
            foreach (var node in attackNodes)
            {
                if (maxIndex < node.Index)
                {
                    maxIndex = node.Index;
                }
            }

            numOfCols = maxIndex - GraphNodeData.DEFAULT_INDEX + 1;
            nodesGrid = new List<GraphNodeData>[numOfCols];
            for (int i = 0; i < numOfCols; i++)
            {
                nodesGrid[i] = new List<GraphNodeData>();
            }

            numOfRows = 0;
            foreach (var node in attackNodes)
            {
                int col = node.Index - GraphNodeData.DEFAULT_INDEX;
                nodesGrid[col].Add(node);
                if (nodesGrid[col].Count > numOfRows)
                {
                    numOfRows = nodesGrid[col].Count;
                }
            }
        }

        const int START_X = 10;
        const int START_Y = 10;

        //private void loadView()
        //{
        //    double horizontalDistance = drawCanvas.ActualWidth / numOfCols;
        //    double verticalDistance;

        //    drawCanvas.Children.Clear();

        //    for (int i = 0; i < numOfCols; i++)
        //    {
        //        verticalDistance = drawCanvas.ActualHeight / numOfRows;
        //        double x, y;
        //        x = START_X + i * horizontalDistance;
        //        for (int j = 0; j < nodesGrid[i].Count; j++)
        //        {
        //            y = START_Y + j * verticalDistance;
        //            Node drawNode = new Node();
        //            drawNode.NodeDelegate = new ClickOnNodeDelegate(NodeClickedHandler);
        //            drawNode.SetName(nodesGrid[i][j].Name);
        //            drawNode.SetPosition(j, i);
        //            drawCanvas.Children.Add(drawNode);
        //            Canvas.SetLeft(drawNode, x);
        //            Canvas.SetTop(drawNode, y);
        //            nodesGrid[i][j].SetPosition(x, y);
        //            drawlineToNode(nodesGrid[i].ElementAt(j));
        //        }

                
        //    }
        //}

        void NodeClickedHandler(string hyperAlertType, string hyperAlertName, DateTime beginTime, DateTime endTime)
        {
            Console.WriteLine("click on node with name: " + hyperAlertName);
        }

        private ArrowLine getArrow(GraphNodeData st, GraphNodeData en)
        {
            ArrowLine line = new ArrowLine();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;

            line.X1 = st.X;
            line.X2 = en.X;
            line.Y1 = st.Y;
            line.Y2 = en.Y;
            Console.WriteLine("line from {0} {1} --> {2} {3}", st.X, st.Y, en.X,en.Y);
            return line;
        }

        private void drawlineToNode(GraphNodeData node)
        {
            List<GraphNodeData> prenodes = node.PreNodes;
            foreach (GraphNodeData pNode in prenodes)
            {
                ArrowLine line = getArrow(pNode, node);
                //drawCanvas.Children.Add(line);
            }
        }
        static BackgroundWorker bWorker;
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    refreshButton.IsEnabled = false;
                    predictButton.IsEnabled = false;
                });
            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);

            bWorker.RunWorkerAsync();
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    refreshButton.IsEnabled = true;
                    predictButton.IsEnabled = true;
                    //this.loadView();
                });
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            loadDataFromServer();
            //initDataToView();

            Console.WriteLine("data preparing completed.");
        }

        private void predictButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    refreshButton.IsEnabled = false;
                    predictButton.IsEnabled = false;
                });

            PredictionWindow predictWindow = new PredictionWindow();
            predictWindow.ParentWindow = new ParentWindowGetFocus(GetFocus);
            predictWindow.Show();
        }
        private void GetFocus()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                (ThreadStart)delegate
                {
                    refreshButton.IsEnabled = true;
                    predictButton.IsEnabled = true;
                });
        }
    }
}
