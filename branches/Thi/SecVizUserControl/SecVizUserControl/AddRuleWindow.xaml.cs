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
using System.Windows.Shapes;

namespace SecVizAdminApp
{
    /// <summary>
    /// Interaction logic for AddRuleWindow.xaml
    /// </summary>
    public partial class AddRuleWindow : Window
    {
        public AddRuleWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextbox.Text != "" && contentTextbox.Text != "")
            {
                RuleAdditer(nameTextbox.Text, contentTextbox.Text);
                this.Close();
            }
            else
            { 
                
            }

        }

        public AddNewRuleDeleagate RuleAdditer;

    }
    public delegate void AddNewRuleDeleagate(string name, string content);
}
