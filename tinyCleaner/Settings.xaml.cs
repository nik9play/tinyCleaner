using System;
using System.Collections.Generic;
using System.Globalization;
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

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                                    .AddDays(version.Build).AddSeconds(version.Revision * 2);

            VersionText.Text = $"tinyCleaner {version} ({buildDate})";
        }

        private void OpenGithub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com");
        }
    }
}
