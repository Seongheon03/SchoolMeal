using Core.Meal;
using Core.Meal.ViewModel;
using SchoolMeal.Common;
using SchoolMeal.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using Core.School.ViewModel;

namespace SchoolMeal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RegistryKey RunRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static Assembly CurAssembly = Assembly.GetExecutingAssembly();
        public static MealViewModel MealViewModel = new MealViewModel();
        public static SchoolViewModel SchoolViewModel = new SchoolViewModel();
        public static SettingViewModel SettingViewModel;

        public App()
        {
            CheckIsAction();
            InitSingleTon();
        }

        private void CheckIsAction()
        {
            Process[] processList = Process.GetProcessesByName(CurAssembly.GetName().Name);

            if (processList.Length >= 2)
            {
                App.Current.Shutdown();
            }
        }

        public static void InitSingleTon()
        {
            Setting.Load();
            SettingViewModel = new SettingViewModel();
        }
    }
}
