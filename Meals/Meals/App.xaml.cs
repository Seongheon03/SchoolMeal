using Core.SchoolMeal;
using Core.SchoolMeal.ViewModel;
using Meals.Common;
using Meals.ViewModel;
using Microsoft.Win32;
using System.Windows;

namespace Meals
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RegistryKey RunRegKey;
        public static string SystemName;
        public static MealViewModel MealViewModel;
        public static SettingViewModel SettingViewModel;

        public App()
        {
            InitSingleTon();
        }

        public static void InitSingleTon()
        {
            Setting.Load();
            RunRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            SystemName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            MealViewModel = new MealData().mealViewModel;
            SettingViewModel = new SettingViewModel();
        }
    }
}
