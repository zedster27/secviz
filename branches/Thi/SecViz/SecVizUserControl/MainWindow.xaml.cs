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

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     
        public MainWindow()
        {            
            InitializeComponent();

            UserInterface ui = new UserInterface(4);
            ui.SetMainButtonName(0, "Scan");
            ui.SetMainButtonName(1, "Knowledge Base");
            ui.SetMainButtonName(2, "Information");
            ui.SetMainButtonName(3, "Protection");

            ui.SetNumOfChildButton(0, 3);
            ui.SetNumOfChildButton(1, 3);
            ui.SetNumOfChildButton(2, 2);
            ui.SetNumOfChildButton(3, 2);

            ui.AddChildButton(0, "Alerts");
            ui.AddChildButton(0, "Attack Graph");
            ui.AddChildButton(0, "Alert Visualization");

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
            AlertVisualization alertVisual = new AlertVisualization();
            ui.TabItemGridList[0].ChildGridList[2].Children.Add(alertVisual);
            RuleView ruleView = new RuleView();
            ui.TabItemGridList[3].ChildGridList[0].Children.Add(ruleView);
            CommandView commandView = new CommandView();
            ui.TabItemGridList[3].ChildGridList[1].Children.Add(commandView);

            ui.TabItemGridList[1].ChildGridList[0].Children.Add(new PredicateView());
            ui.TabItemGridList[1].ChildGridList[1].Children.Add(new ImplicationView());
            ui.TabItemGridList[1].ChildGridList[2].Children.Add(new HyperAlertView());

            ui.FinalizeUI();           
            uiGrid.Children.Add(ui);          
        }
    }
}
