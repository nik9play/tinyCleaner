using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
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
using MahApps.Metro.IconPacks;

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

        PowerShell ps = PowerShell.Create();

        private void AppTileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/tinyCleaner;component/Win10Icons/" + PackageName + ".png"));
        }

        private void ActionBtn(object sender, RoutedEventArgs e)
        {
            string hui = "";
            ps.AddCommand("Get-AppxPackage").AddParameter("AllUsers").AddArgument(PackageName).AddCommand("Remove-AppxPackage");

            foreach (PSObject result in ps.Invoke())
            {
                hui += result;
            }

            if (hui == "")
                Icon.Kind =  PackIconMaterialKind.ArrowCollapseDown;

        }

        //register Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AppTile), new PropertyMetadata("Untitled"));

        public string PackageName
        {
            get { return (string)GetValue(PackageNameProperty); }
            set { SetValue(PackageNameProperty, value); }
        }

        public static readonly DependencyProperty PackageNameProperty =
            DependencyProperty.Register("PackageName", typeof(string), typeof(AppTile), new PropertyMetadata("None"));
    }
}
