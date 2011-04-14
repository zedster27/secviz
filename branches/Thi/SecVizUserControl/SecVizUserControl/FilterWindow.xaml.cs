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
    /// Interaction logic for FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {

        public FilterData MainView { set; get; }
        
        public FilterWindow()
        {
            InitializeComponent();
        }
        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
            
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < propertyList.Count; i++)
            {
                string key = propertyList.ElementAt(i);
                string val = propTextboxValues.ElementAt(i).Text;
                dict.Add(key, val);
            }
            int option = optionCombox.SelectedIndex;
            MainView(dict, option);
            //this.Hide();
        }


        public void SetListPropertyToFilter(List<string> propNames)
        {
            int cnt = propNames.Count;
            //propLabelNames = new List<Label>();
            propTextboxValues = new List<TextBox>();
            propertyList = propNames;

            int i = 0;
            foreach (string propName in propertyList)
            {
                Label propLb = new Label();
                propLb.Content = propName;
                propLb.Margin = new Thickness(X_LABEL_START_POSITION , Y_LABEL_START_POSITION + i * (PROP_NAME_LABEL_HEIGHT + 10),
                                              PROP_NAME_LABEL_WIDTH, PROP_NAME_LABEL_HEIGHT);
                mainGrid.Children.Add(propLb);

                TextBox propTb = new TextBox();
                propTb.Margin = new Thickness(X_TEXTBOX_START_POSITION, Y_TEXTBOX_START_POSITION + i * (PROP_VALUE_TEXTBOX_HEIGHT + 10),
                                              PROP_VALUE_TEXTBOX_WIDTH, PROP_VALUE_TEXTBOX_HEIGHT);
                mainGrid.Children.Add(propTb);
                propTextboxValues.Add(propTb);
                i++;
            }

        }

        const int X_LABEL_START_POSITION = 25;
        const int Y_LABEL_START_POSITION = 45;
        const int PROP_NAME_LABEL_WIDTH = 200;
        const int PROP_NAME_LABEL_HEIGHT = 30;

        const int X_TEXTBOX_START_POSITION = 230;
        const int Y_TEXTBOX_START_POSITION = 45;
        const int PROP_VALUE_TEXTBOX_WIDTH = 100;
        const int PROP_VALUE_TEXTBOX_HEIGHT = 30;

        Window mainWindow;
       // private List<Label> propLabelNames;
        private List<TextBox> propTextboxValues;
        private List<string> propertyList;
    }

    public delegate void FilterData(Dictionary<string, string> att, int constraint);
}
