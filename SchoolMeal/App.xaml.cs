using Core.Meal.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using Core.School.ViewModel;
using SchoolMeal.Properties;

namespace SchoolMeal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RegistryKey runRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static Assembly curAssembly = Assembly.GetExecutingAssembly();
        public static MealViewModel mealViewModel = new MealViewModel();
        public static SchoolViewModel schoolViewModel = new SchoolViewModel();

        public App()
        {
            CheckIsAction();
            Initialize();
        }

        private void CheckIsAction()
        {
            Process[] processList = Process.GetProcessesByName(curAssembly.GetName().Name);

            if (processList.Length >= 2)
            {
                App.Current.Shutdown();
            }
        }

        public static void Initialize()
        {
            Settings.Default.isWindowVisible = true;

            if (Settings.Default.currentSchool == null)
            {
                Settings.Default.currentSchool = new Core.School.Model.School();
            }
        }
    }
}
