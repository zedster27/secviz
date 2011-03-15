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
        public MainWindow()
        {            
            InitializeComponent();
            this.Width = 1000;
            this.Height = 650;
            mainGrid.Width = 1000;
            mainGrid.Height = 650;

            UserInterface ui = new UserInterface(4);
            ui.SetMainButtonName(0, "Scan");
            ui.SetMainButtonName(1, "Knowledge Base");
            ui.SetMainButtonName(2, "Information");
            ui.SetMainButtonName(3, "Protection");

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

            AlertView alertView = new AlertView();
            ui.TabItemGridList[0].ChildGridList[0].Children.Add(alertView);
            AttackGraphView attackGraphView = new AttackGraphView();
            ui.TabItemGridList[0].ChildGridList[1].Children.Add(attackGraphView);
            RuleView ruleView = new RuleView();
            ui.TabItemGridList[3].ChildGridList[0].Children.Add(ruleView);
            CommandView commandView = new CommandView();
            ui.TabItemGridList[3].ChildGridList[1].Children.Add(commandView);

            ui.FinalizeUI();           
            uiGrid.Children.Add(ui);          
        }
    }
}
