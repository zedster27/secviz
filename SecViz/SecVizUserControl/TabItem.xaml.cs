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
    public delegate void MainButtonClickDelegate(int mainButtonIndex);
    public delegate void ChildButtonClickDelegate(int mainButtonIndex, int childButtonIndex);
    /// <summary>
    /// Interaction logic for TabItem.xaml
    /// </summary>
    public partial class TabItem : UserControl
    {
        public MainButtonClickDelegate MainButtonDelegate;
        public ChildButtonClickDelegate ChildButtonDelegate;
        public TabItem()
        {
            InitializeComponent();
            ImageBrush brush = new ImageBrush();
            Uri uri = new Uri(Constant.MAIN_TAB_PATH);
            BitmapImage img = new BitmapImage(uri);
            brush.ImageSource = img;
            main_button.Background = brush;

            this.Width = ITEM_WIDTH + ITEM_HEIGHT;
            this.Height = ITEM_HEIGHT;
            main_listView.Width = ITEM_WIDTH;
            main_listView.Height = ITEM_HEIGHT;
            main_listView.Margin = new Thickness(ITEM_HEIGHT, ITEM_HEIGHT, 0, -ITEM_HEIGHT);
            main_listView.Background = Brushes.LightGray;

            main_listView.Visibility = Visibility.Hidden;
            
            mainItemGrid.Height = ITEM_HEIGHT;
            mainItemGrid.Width = ITEM_WIDTH + ITEM_HEIGHT;
            
            NumOfItem = 0;
            IsSelected = false;
            SelectedButton = -1;
        }
        /// <summary>
        /// set the content of the main button
        /// </summary>
        /// <param name="name"></param>
        public void SetMainButtonName(string name)
        {
            main_button.Content = name;
        }
        /// <summary>
        /// add new child button
        /// </summary>
        /// <param name="name"></param>
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
        //the index of this tabItem in the list of tabItem in UserInterface
        public int Index;
        private const double ITEM_WIDTH = 120;
        private const double ITEM_HEIGHT = 30;
        /// <summary>
        /// handler of event click on main button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.MainButtonDelegate(Index);

        }          
        private void main_listView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedButton = main_listView.SelectedIndex;            
            this.ChildButtonDelegate(this.Index, SelectedButton);
        }
    }
}
