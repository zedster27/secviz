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
            NumOfButton = 0;

            this.Width = BUTTON_WIDTH + 30;
            this.Height = BUTTON_HEIGHT;
            main_button.Width = BUTTON_WIDTH;
            main_button.Height = BUTTON_HEIGHT;
            main_button.Content = nameOfMainButton;
            buttonList = new List<Button>();        
        }
        public void AddButton(string name)
        {
            //plus the height
            this.Height += BUTTON_HEIGHT;

            //change position of main button
            main_button.Margin = new Thickness(0, 0, 30, (NumOfButton + 1) * BUTTON_HEIGHT);

            //change position of previous child button
            int i = 0;
            foreach (Button tempButton in buttonList)
            {
                tempButton.Margin = new Thickness(30, (i + 1) * BUTTON_HEIGHT, 0, (NumOfButton - i) * BUTTON_HEIGHT);
                i++;
            }

            //create new button
            Button newButton = new Button();
            newButton.Width = BUTTON_WIDTH;
            newButton.Height = BUTTON_HEIGHT;
            newButton.Content = name;
            newButton.Margin = new Thickness(30, 
                                             (NumOfButton+1) * BUTTON_HEIGHT, 
                                             0,
                                             0);            
            buttonList.Add(newButton);
            
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
            IsSelected = !IsSelected;
            if (IsSelected == true)
            {
                foreach (Button tempButton in buttonList)
                {
                    tempButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (Button tempButton in buttonList)
                {
                    tempButton.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
