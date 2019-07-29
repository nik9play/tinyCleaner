using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace tinyCleaner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Settings win = new Settings();
            win.Show();
        }

        private void CheckAll(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            btn.Click -= CheckAll;
            btn.Click += UnCheckAll;
            btn.Content = "Снять все галочки";
            btn.Width = 137;
            var GridParent = (Grid)btn.GetParentObject();
            switch (GridParent.Name)
            {
                case "Debloat":
                    foreach (var a in Debloat.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = true;
                    }
                    break;
                case "Clean":
                    foreach (var a in Clean.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = true;
                    }
                    break;
                case "UWP":
                    foreach (var a in UWP.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = true;
                    }
                    break;
            }
        }

        private void UnCheckAll(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            btn.Click -= UnCheckAll;
            btn.Click += CheckAll;
            btn.Content = "Поставить все галочки";
            btn.Width = 168;
            var GridParent = (Grid)btn.GetParentObject();
            switch (GridParent.Name)
            {
                case "Debloat":
                    foreach (var a in Debloat.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = false;
                    }
                    break;
                case "Clean":
                    foreach (var a in Clean.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = false;
                    }
                    break;
                case "UWP":
                    foreach (var a in UWP.Children)
                    {
                        if (a.GetType() == typeof(CheckBox))
                            ((CheckBox)a).IsChecked = false;
                    }
                    break;
            }
        }

        private void DebugBtn(object sender, RoutedEventArgs e)
        {
            RegistryKey RegistryLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey key = RegistryLM.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true);
            key.DeleteSubKeyTree(@"Sense", false);
        }

        private void ApplyDebloat(object sender, RoutedEventArgs e)
        {
            bool NothingChecked = true;

            RegistryKey RegistryLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey RegistryCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

            foreach (var a in Debloat.Children)
            {
                if (a.GetType() == typeof(CheckBox))
                    if (((CheckBox)a).IsChecked == true)
                        NothingChecked = false;
            }

            if (NothingChecked == true)
                this.ShowMessageAsync("tinyCleaner", "Вы не выбрали ни одной галочки.");

            foreach (var a in Debloat.Children)
            {
                if (a.GetType() == typeof(CheckBox))
                    ((CheckBox)a).IsEnabled = false;
                if (a.GetType() == typeof(Button))
                    ((Button)a).IsEnabled = false;
            }

            if (DisableWindowsDefender.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
                key.SetValue("SmartScreenEnabled", "off");
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true);
                key.SetValue("DisableAntiSpyware", 1);

                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AppHost", true);
                key.SetValue("EnableWebContentEvaluation", 0);
                key.Close();

                key = RegistryCU.CreateSubKey(@"Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppContainer\Storage\microsoft.microsoftedge_8wekyb3d8bbwe\MicrosoftEdge\PhishingFilter", true);
                key.SetValue("EnabledV9", 0);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", true);
                key.SetValue("SpyNetReporting", 0);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", true);
                key.SetValue("SubmitSamplesConsent", 2);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", true);
                key.SetValue("DontReportInfectionInformation", 1);
                key.Close();

                key = RegistryLM.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true);
                key.DeleteSubKeyTree(@"Sense", false);

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MRT", true);
                key.SetValue("DontReportInfectionInformation", 1);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MRT", true);
                key.SetValue("DontOfferThroughWUAU", 1);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("SecurityHealth");
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run", true);
                key.DeleteValue("SecurityHealth");
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\SecHealthUI.exe", true);
                key.SetValue("Debugger", @"%windir%\System32\taskkill.exe");
                key.Close();

                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance", true);
                key.SetValue("Enabled", 0);
                key.Close();

                key = RegistryLM.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true);
                key.DeleteSubKeyTree(@"SecurityHealthService", false);
            }

            if (DisableSettingsPrivacy.IsChecked == true) {
                RegistryKey key;

                //Рекомендации в пуске
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
                key.SetValue("SubscribedContent-338388Enabled", 0);
                key.Close();

                //Индентификатор рекламы
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true);
                key.SetValue("Enabled", 0);
                key.Close();

                //Отслеживание запуска приложений
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
                key.SetValue("Start_TrackProgs", 0);
                key.Close();

                //Рекомендуемое содержимое в параметрах
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
                key.SetValue("SubscribedContent-338393Enabled", 0);
                key.SetValue("SubscribedContent-353694Enabled", 0);
                key.SetValue("SubscribedContent-353696Enabled", 0);
                key.Close();

                //Голос через инет
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy", true);
                key.SetValue("HasAccepted", 0);
                key.Close();

                //Персонализация ввода
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Personalization\Settings", true);
                key.SetValue("AcceptedPrivacyPolicy", 0);
                key.Close();

                key = RegistryCU.CreateSubKey(@"Software\Microsoft\InputPersonalization", true);
                key.SetValue("RestrictImplicitTextCollection", 1);
                key.SetValue("RestrictImplicitInkCollection", 1);
                key.Close();

                key = RegistryCU.CreateSubKey(@"Software\Microsoft\InputPersonalization\TrainedDataStore", true);
                key.SetValue("HarvestContacts", 0);
                key.Close();

                //улучшение рукописного ввода
                key = RegistryCU.CreateSubKey(@"Software\Microsoft\Input\TIPC", true);
                key.SetValue("Enabled", 0);
                key.Close();
            }

            if (DisableDVR.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryCU.CreateSubKey(@"System\GameConfigStore", true);
                key.SetValue("GameDVR_Enabled", 0);
                key.Close();
            }

            if (DisableCortana.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
                key.SetValue("AllowCortana", 0);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Search", true);
                key.SetValue("BingSearchEnabled", 0);
                key.Close();
            }

            if (DisableErrorReport.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting", true);
                key.SetValue("Disabled", 1);
                key.Close();

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", true);
                key.SetValue("Disabled", 1);
                key.Close();
            }

            if (DisableAutoUpdates.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", true);
                key.SetValue("NoAutoUpdate", 0);
                key.SetValue("AUOptions", 2);
                key.SetValue("ScheduledInstallDay", 0);
                key.SetValue("ScheduledInstallTime", 3);
                key.Close();
            }

            if (DisableSync.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"Software\Policies\Microsoft\Windows\SettingSync", true);
                key.SetValue("DisableSettingSync", 2);
                key.SetValue("DisableSettingSyncUserOverride", 1);
                key.Close();
            }

            if (DisableWindowsTips.IsChecked == true)
            {
                RegistryKey key;

                key = RegistryLM.CreateSubKey(@"Software\Policies\Microsoft\Windows\CloudContent", true);
                key.SetValue("DisableSoftLanding", 1);
                key.SetValue("DisableWindowsSpotlightFeatures", 1);
                key.SetValue("DisableWindowsConsumerFeatures", 1);
                key.Close();

                key = RegistryLM.CreateSubKey(@"Software\Policies\Microsoft\Windows\DataCollection", true);
                key.SetValue("DoNotShowFeedbackNotifications", 1);
                key.Close();

                key = RegistryLM.CreateSubKey(@"Software\Policies\Microsoft\WindowsInkWorkspace", true);
                key.SetValue("AllowSuggestedAppsInWindowsInkWorkspace", 0);
                key.Close();
            }

            if (DisableServices.IsChecked == true)
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C ipconfig.exe";
                process.StartInfo = startInfo;
                process.Start();
                var test = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                MessageBox.Show(test, "test");
            }

            foreach (var a in Debloat.Children)
            {
                if (a.GetType() == typeof(CheckBox))
                    ((CheckBox)a).IsEnabled = true;
                if (a.GetType() == typeof(Button))
                    ((Button)a).IsEnabled = true;
            }
        }
    }
}
