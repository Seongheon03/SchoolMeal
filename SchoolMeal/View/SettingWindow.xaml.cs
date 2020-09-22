using SchoolMeal.Properties;
using System;
using System.Windows;

namespace SchoolMeal.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            Loaded += SettingWindow_Loaded;
            Closed += SettingWindow_Closed;
        }

        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            SetNavi();

            DataContext = this;
        }

        private void SettingWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
