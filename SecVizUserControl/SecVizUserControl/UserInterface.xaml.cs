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
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : UserControl
    {
        public UserInterface(int inputNumOfTabItem)
        {
            InitializeComponent();
            this.NumOfTabItem = inputNumOfTabItem;
            TabItemList = new TabItem[NumOfTabItem];
            for (int i = 0; i < NumOfTabItem; i++)
            {
                TabItemList[i] = new TabItem();
                TabItemList[i].Index = i;
                TabItemList[i].MainButtonDelegate = this.MainButtonClickHandler;
                TabItemList[i].ChildButtonDelegate = this.ChildButtonClickHandler;
            }
            TabItemGridList = new GridCollection[NumOfTabItem];            
        }
        /// <summary>
        /// Create the new grids for main button with mainButtonIndex base on the numOfChildButton
        /// </summary>
        /// <param name="mainButtonIndex"></param>
        /// <param name="numOfChildButton"></param>
        public void SetNumOfChildButton(int mainButtonIndex, int numOfChildButton)
        {
            try
            {
                TabItemGridList[mainButtonIndex] = new GridCollection(numOfChildButton);
                
                TabItemGridList[mainButtonIndex].MainGrid.Visibility = Visibility.Hidden;
                mainUIGrid.Children.Add(TabItemGridList[mainButtonIndex].MainGrid);

                for (int i = 0; i < TabItemGridList[mainButtonIndex].NumOfChildGrid; i++)
                {
                    TabItemGridList[mainButtonIndex].ChildGridList[i].Visibility = Visibility.Hidden;
                    mainUIGrid.Children.Add(TabItemGridList[mainButtonIndex].ChildGridList[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public void SetMainButtonName(int index, string name)
        {
            try
            {
                TabItemList[index].SetMainButtonName(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// add a new child button inside the mainbutton with the index
        /// </summary>
        /// <param name="mainButtonIndex"></param>
        /// <param name="name"></param>
        public void AddChildButton(int mainButtonIndex, string name)
        {
            try
            {
                if (TabItemGridList[mainButtonIndex].NumOfChildGrid <= TabItemList[mainButtonIndex].NumOfItem)
                {
                    Console.WriteLine("Number of child button is full");                    
                }
                else
                {
                    TabItemList[mainButtonIndex].AddItem(name);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// this method will add the elements of UserInteface to the grid
        /// </summary>
        public void FinalizeUI()
        {
            this.Width = UI_WIDTH;
            this.Height = UI_HEIGHT;
            mainUIGrid.Width = UI_WIDTH;
            mainUIGrid.Height = UI_HEIGHT;
            for (int i = 0; i < NumOfTabItem; i++)
            {
                TabItem item = TabItemList[i];
                item.Margin = new Thickness(0, i * TAB_ITEM_HEIGHT, UI_WIDTH - TAB_ITEM_WIDTH, UI_HEIGHT - (i+1)*TAB_ITEM_HEIGHT);             
                mainUIGrid.Children.Add(item);
            }
        }
        /// <summary>
        /// event handler for the delegate mainButtonClick of TabItem
        /// </summary>
        /// <param name="mainButtonIndex"></param>
        public void MainButtonClickHandler(int mainButtonIndex)
        {          
            //set posiotion of tabItem
            double previousHeight = TabItemList[mainButtonIndex].Margin.Top;               
            TabItemList[mainButtonIndex].Margin = new Thickness(0, 
                                                                previousHeight, 
                                                                UI_WIDTH - TAB_ITEM_WIDTH, 
                                                                UI_HEIGHT - previousHeight - TabItemList[mainButtonIndex].Height);
            double padHeight = 0;
            for (int i = mainButtonIndex + 1; i < NumOfTabItem; i++)
            {
                TabItemList[i].Margin = new Thickness(0,
                                                      previousHeight + TabItemList[mainButtonIndex].Height + padHeight,
                                                      UI_WIDTH - TAB_ITEM_WIDTH,
                                                      UI_HEIGHT - previousHeight - TabItemList[mainButtonIndex].Height - padHeight - TabItemList[i].Height);
                padHeight += TabItemList[i].Height;
            }                        
            //set the right grid to visible          
            if (lastVisibleGrid != null)
            {
                lastVisibleGrid.Visibility = Visibility.Hidden;
            }
            TabItemGridList[mainButtonIndex].MainGrid.Margin = new Thickness(TAB_ITEM_WIDTH, 0, 0, 0);
            TabItemGridList[mainButtonIndex].MainGrid.Visibility = Visibility.Visible;
            lastVisibleGrid = TabItemGridList[mainButtonIndex].MainGrid;                                                                                                                                                                             
        }
        /// <summary>
        /// event handler for the delegate childButtonClick of TabItem
        /// </summary>
        /// <param name="mainButtonIndex"></param>
        /// <param name="childButtonIndex"></param>
        public void ChildButtonClickHandler(int mainButtonIndex, int childButtonIndex)
        {
            if (lastVisibleGrid != null)
            {
                lastVisibleGrid.Visibility = Visibility.Hidden;
            }
            TabItemGridList[mainButtonIndex].ChildGridList[childButtonIndex].Margin = new Thickness(TAB_ITEM_WIDTH, 0, 0, 0);
            TabItemGridList[mainButtonIndex].ChildGridList[childButtonIndex].Visibility = Visibility.Visible;
            lastVisibleGrid = TabItemGridList[mainButtonIndex].ChildGridList[childButtonIndex];
        }
        public TabItem[] TabItemList;
        public GridCollection[] TabItemGridList;
        public int NumOfTabItem;
        private Grid lastVisibleGrid = null;

        const double TAB_ITEM_WIDTH = 150;
        const double TAB_ITEM_HEIGHT = 30;
        const double GRID_WIDTH = 800;
        const double GRID_HEIGHT = 500;
        const double UI_WIDTH = 1000;
        const double UI_HEIGHT = 600;
    }
}
