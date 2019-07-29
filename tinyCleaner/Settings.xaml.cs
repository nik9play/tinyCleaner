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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace tinyCleaner
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>


    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void OpenGithub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com");
        }
    }
}