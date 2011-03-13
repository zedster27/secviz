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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void AddLabel(Grid grid, string name)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = name;
            tmpLabel.HorizontalAlignment = HorizontalAlignment.Center;
            tmpLabel.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(tmpLabel);
        }

        public MainWindow()
        {            
            InitializeComponent();
            this.Width = 1000;
            this.Height = 600;
            mainGrid.Width = 1000;
            mainGrid.Height = 600;

            UserInterface ui = new UserInterface(4);
            ui.SetMainButtonName(0, "Scan");
            ui.SetMainButtonName(1, "Knowledge Base");
            ui.SetMainButtonName(2, "Information");
            ui.SetMainButtonName(2, "Protection");

            ui.SetNumOfChildButton(0, 2);
            ui.SetNumOfChildButton(1, 3);
            ui.SetNumOfChildButton(2, 2);
            ui.SetNumOfChildButton(3, 2);

            ui.AddChildButton(0, "Alerts");
            ui.AddChildButton(0, "Attack Graph");            

            ui.AddChildButton(1, "Predicate");
            ui.AddChildButton(1, "Implication");
            ui.AddChildButton(1, "Hyper Alert Type");

            ui.AddChildButton(2, "Topology");
            ui.AddChildButton(2, "Configuration");

            ui.AddChildButton(3, "Rules");
            ui.AddChildButton(3, "Commands");
            
            /*AddLabel(ui.TabItemGridList[0].MainGrid, "GRID BUTTON A");
            AddLabel(ui.TabItemGridList[1].MainGrid, "GRID BUTTON B");
            AddLabel(ui.TabItemGridList[2].MainGrid, "GRID BUTTON C");

            AddLabel(ui.TabItemGridList[0].ChildGridList[0], "GRID BUTTON A.0");
            AddLabel(ui.TabItemGridList[0].ChildGridList[1], "GRID BUTTON A.1");
            AddLabel(ui.TabItemGridList[0].ChildGridList[2], "GRID BUTTON A.2");

            AddLabel(ui.TabItemGridList[1].ChildGridList[0], "GRID BUTTON B.0");
            AddLabel(ui.TabItemGridList[1].ChildGridList[1], "GRID BUTTON B.1");

            AddLabel(ui.TabItemGridList[2].ChildGridList[0], "GRID BUTTON C.0");
            AddLabel(ui.TabItemGridList[2].ChildGridList[1], "GRID BUTTON C.1");
            AddLabel(ui.TabItemGridList[2].ChildGridList[2], "GRID BUTTON C.2");*/

            ui.FinalizeUI();            
            uiGrid.Children.Add(ui);           
        }
    }
}
