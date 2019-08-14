﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tinyCleaner
{
    /// <summary>
    /// Логика взаимодействия для AppTile.xaml
    /// </summary>
    public partial class AppTile : UserControl
    {
        public AppTile()
        {
            InitializeComponent();
        }

        private void Anal(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Text, "test");
        }

        //register Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AppTile), new PropertyMetadata("Untitled"));
    }
}
