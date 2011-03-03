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
            IsSelected = false;
            this.BorderBrush = Brushes.Blue;

            this.Width = BUTTON_WIDTH + 30;
            this.Height = BUTTON_HEIGHT;
            main_button.Width = BUTTON_WIDTH;
            main_button.Height = BUTTON_HEIGHT;
            main_button.Content = nameOfMainButton;
            buttonList = new List<Button>();
        }
        public void AddButton(string name)
        {
            Button newButton = new Button();
            newButton.Width = BUTTON_WIDTH;
            newButton.Height = BUTTON_HEIGHT;
            newButton.Content = name;
            newButton.Margin = new Thickness(30, 
                                             (NumOfButton+1) * BUTTON_HEIGHT, 
                                             BUTTON_WIDTH + 30,
                                             (NumOfButton + 2) * BUTTON_HEIGHT);
            newButton.Visibility = Visibility.Collapsed;

            buttonList.Add(newButton);

            this.Height += BUTTON_HEIGHT;
            mainItemGrid.Children.Add(newButton);           

            NumOfButton++;
        }
        private List<Button> buttonList = null;
        public int SelectedButton;
        public int NumOfButton = 0;
        public bool IsSelected;
        private const double BUTTON_WIDTH = 120;
        private const double BUTTON_HEIGHT = 30;

        private void main_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
