using Core.Meal.ViewModel;
using SchoolMeal.Common;
using SchoolMeal.ViewModel;
using System;
using System.Windows;
using System.Windows.Media;

namespace SchoolMeal.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private MealViewModel _mealViewModel = App.MealViewModel;
        public MealViewModel MealViewModel
        {
            get => _mealViewModel;
            set => _mealViewModel = value;
        }

        private SettingViewModel _settingViewModel = App.SettingViewModel;
        public SettingViewModel SettingViewModel
        {
            get => _settingViewModel;
            set => _settingViewModel = value;
        }

        public SettingWindow()
        {
            InitializeComponent();
            Loaded += SettingWindow_Loaded;
            Closed += SettingWindow_Closed;
        }

        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;

            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void SettingWindow_Closed(object sender, EventArgs e)
        {
            Setting.isStartingProgram = tbtnStartingProgram.Status;
            Setting.backgroundColor = (Color)cpBackgroundColor.SelectedColor;
            Setting.fontColor = (Color)cpFontColor.SelectedColor;

            Setting.Save();
        }
    }
}
