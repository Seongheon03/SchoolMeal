using Core.SchoolMeal;
using Core.SchoolMeal.ViewModel;
using Meals.Common;
using Meals.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;

namespace Meals
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RegistryKey RunRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static string SystemName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
        public static MealViewModel MealViewModel;
        public static SettingViewModel SettingViewModel;

        public App()
        {
            CheckIsAction();
            InitSingleTon();
        }

        private void CheckIsAction()
        {
            Process[] processList = Process.GetProcessesByName(SystemName);

            if (processList.Length >= 2)
            {
                App.Current.Shutdown();
            }
        }

        public static void InitSingleTon()
        {
            Setting.Load();
            MealViewModel = new MealData().mealViewModel;
            SettingViewModel = new SettingViewModel();
        }
    }
}
