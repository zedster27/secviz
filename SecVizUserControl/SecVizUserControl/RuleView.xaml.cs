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
using System.Threading;
using System.Windows.Threading;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for RuleView.xaml
    /// </summary>
    public partial class RuleView : UserControl
    {
        public RuleView()
        {
            InitializeComponent();
            loadedRule = new List<Rule>();
        }

        private void deleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            int ind = ruleDataGrid.SelectedIndex;
            if (ind > 0)
            {
                loadedRule.RemoveAt(ind);
                this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                      (ThreadStart)delegate
                      {
                          this.DataContext = new
                          {
                              Rules = loadedRule
                          };
                      });
            }
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindow addRuleWindow = new AddRuleWindow();
            addRuleWindow.RuleAdditer = new AddNewRuleDeleagate(addRuleHandler);
            addRuleWindow.Show();
        }

        private void addRuleHandler(string name, string content)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.DataBind,
                 (ThreadStart)delegate
                 {
                     this.DataContext = new
                     {
                         Rules = loadedRule
                     };
                 });
        }

        private List<Rule> loadedRule;
    }

    public class Rule
    {
        public string Name;
        public string Content;
    }
}
