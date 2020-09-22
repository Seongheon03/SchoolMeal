using System;
using System.Windows;
using System.Windows.Forms;

namespace SchoolMeal.View
{
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon = new NotifyIcon();
        private readonly ContextMenu Menu = new ContextMenu();

        private void SetTraySystem()
        {
            SetContextMenu();

            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Text = "School Meal";
            notifyIcon.ContextMenu = Menu;
            notifyIcon.Visible = true;
        }

        private void HideTraySystem()
        {
            notifyIcon.Visible = false;
            notifyIcon.Icon = null;
        }

        private void SetContextMenu()
        {
            MenuItem settingItem = new MenuItem("Setting");
            MenuItem exitItem = new MenuItem("Exit");

            settingItem.Click += SettingItem_Click;
            exitItem.Click += ExitItem_Click;

            Menu.MenuItems.Add(settingItem);
            Menu.MenuItems.Add(exitItem);
        }

        private void SettingItem_Click(object sender, EventArgs e)
        {
            new SettingWindow().Show();
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
