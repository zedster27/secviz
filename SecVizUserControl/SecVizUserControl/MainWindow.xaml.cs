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

            UserInterface ui = new UserInterface(3);
            ui.SetMainButtonName(0, "A");
            ui.SetMainButtonName(1, "B");
            ui.SetMainButtonName(2, "C");

            ui.SetNumOfChildButton(0, 3);
            ui.SetNumOfChildButton(1, 2);
            ui.SetNumOfChildButton(2, 3);

            ui.AddChildButton(0, "A.0");
            ui.AddChildButton(0, "A.1");
            ui.AddChildButton(0, "A.2");

            ui.AddChildButton(1, "B.0");
            ui.AddChildButton(1, "B.1");            

            ui.AddChildButton(2, "C.0");
            ui.AddChildButton(2, "C.1");
            ui.AddChildButton(2, "C.2");

            AddLabel(ui.TabItemGridList[0].MainGrid, "GRID BUTTON A");
            AddLabel(ui.TabItemGridList[1].MainGrid, "GRID BUTTON B");
            AddLabel(ui.TabItemGridList[2].MainGrid, "GRID BUTTON C");

            AddLabel(ui.TabItemGridList[0].ChildGridList[0], "GRID BUTTON A.0");
            AddLabel(ui.TabItemGridList[0].ChildGridList[1], "GRID BUTTON A.1");
            AddLabel(ui.TabItemGridList[0].ChildGridList[2], "GRID BUTTON A.2");

            AddLabel(ui.TabItemGridList[1].ChildGridList[0], "GRID BUTTON B.0");
            AddLabel(ui.TabItemGridList[1].ChildGridList[1], "GRID BUTTON B.1");

            AddLabel(ui.TabItemGridList[2].ChildGridList[0], "GRID BUTTON C.0");
            AddLabel(ui.TabItemGridList[2].ChildGridList[1], "GRID BUTTON C.1");
            AddLabel(ui.TabItemGridList[2].ChildGridList[2], "GRID BUTTON C.2");

            ui.FinalizeUI();            
            mainGrid.Children.Add(ui);           
        }
    }
}
