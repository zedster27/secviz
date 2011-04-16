﻿using System;
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
    /// Interaction logic for CommandView.xaml
    /// </summary>
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();
            ImageBrush brush = new ImageBrush();
            Uri uri = new Uri(Constant.BUTTON_BACKGROUND_PATH);
            BitmapImage img = new BitmapImage(uri);
            brush.ImageSource = img;
            refreshButton.Background = brush;
            addcommandButton.Background = brush;

            ImageBrush brushBackground = new ImageBrush();
            Uri uriBackground = new Uri(Constant.BACKGROUND_PATH);
            BitmapImage imgBackground = new BitmapImage(uriBackground);
            brushBackground.ImageSource = imgBackground;
            this.Background = brushBackground;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addcommandButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
