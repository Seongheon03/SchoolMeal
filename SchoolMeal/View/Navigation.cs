using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMeal.View
{
    public partial class SettingWindow : Window
    {
        public List<NaviData> NaviItems { get; set; } = new List<NaviData>();

        private void SetNavi()
        {
            NaviItems.Add(new NaviData() { Idx = Menu.Home, ImagePath = "/Resources/home.png" });
            NaviItems.Add(new NaviData() { Idx = Menu.School, ImagePath = "/Resources/school.png" });
        }

        private void lbNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Menu selectedMenu = ((NaviData)lbNavi.SelectedItem).Idx;

            CollapseAllControl();

            switch (selectedMenu)
            {
                case Menu.Home:
                    ctrlSettingHome.Visibility = Visibility.Visible;
                    App.schoolViewModel.EnteredSchoolName = "";
                    break;
                case Menu.School:
                    ctrlSettingSchool.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CollapseAllControl()
        {
            ctrlSettingHome.Visibility = Visibility.Collapsed;
            ctrlSettingSchool.Visibility = Visibility.Collapsed;
        }
    }

    public enum Menu
    {
        Home, School
    }

    public class NaviData
    {
        public Menu Idx { get; set; }
        public string ImagePath { get; set; }
    }
}
