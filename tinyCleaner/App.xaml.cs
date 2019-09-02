using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;
using Microsoft.Win32;

namespace tinyCleaner
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

            MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString());

            RegistryKey RegistryCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey key = RegistryCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);

            if (key.GetValue("AppsUseLightTheme").ToString() == "1")
            {
                ThemeManager.ChangeAppTheme(Application.Current, "BaseLight");
            }

            base.OnStartup(e);
        }
    }
}
