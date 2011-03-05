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
    /// Interaction logic for TabItem.xaml
    /// </summary>
    public partial class TabItem : UserControl
    {
        public TabItem(string nameOfMainButton)
        {
            InitializeComponent();
            this.Width = ITEM_WIDTH + ITEM_HEIGHT;
            this.Height = ITEM_HEIGHT;
            main_listView.Width = ITEM_WIDTH;
            main_listView.Height = 0;
            main_listView.Visibility = Visibility.Hidden;
            
            mainItemGrid.Height = ITEM_HEIGHT;
            mainItemGrid.Width = ITEM_WIDTH + ITEM_HEIGHT;
            main_button.Content = nameOfMainButton;
            NumOfItem = 0;
            IsSelected = false;
            SelectedButton = -1;
        }
        public void AddItem(string name)
        {
            NumOfItem++;
            ListViewItem newItem = new ListViewItem();
            newItem.Content = name;            
            newItem.Height = ITEM_HEIGHT;

            main_listView.Items.Add(newItem);            
            main_listView.Height += ITEM_HEIGHT;            
        }
        
        public int SelectedButton;
        public int NumOfItem;
        public bool IsSelected;
        private const double ITEM_WIDTH = 120;
        private const double ITEM_HEIGHT = 30;

        private void main_button_Click(object sender, RoutedEventArgs e)
        {
            IsSelected = !IsSelected;
            if (IsSelected == true)
            {
                this.Height = (NumOfItem + 1) * ITEM_HEIGHT;
                mainItemGrid.Height = (NumOfItem + 1) * ITEM_HEIGHT;
                main_listView.Visibility = Visibility.Visible;
            }
            else
            {
                this.Height = ITEM_HEIGHT;
                mainItemGrid.Height = ITEM_HEIGHT;
                main_listView.Visibility = Visibility.Hidden;
            }
        }

        private void main_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
