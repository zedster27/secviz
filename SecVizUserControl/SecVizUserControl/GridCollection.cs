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
    public class GridCollection
    {
        public GridCollection(int inputNumOfChildGrid)
        {
            this.NumOfChildGrid = inputNumOfChildGrid;
            MainGrid = new Grid();
            ChildGridList = new Grid[NumOfChildGrid];
            for (int i = 0; i<NumOfChildGrid ; i++)
                ChildGridList[i] = new Grid();
        }
        public Grid MainGrid;
        public Grid[] ChildGridList;
        public int NumOfChildGrid;
    }
}
