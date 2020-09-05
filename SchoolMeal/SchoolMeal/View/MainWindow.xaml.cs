using Core.SchoolMeal.ViewModel;
using SchoolMeal.ViewModel;
using System;
using System.Windows;

namespace SchoolMeal.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();
            InitWorkerW();
            InitOnDisplaySettingChanged();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            ShowOnWorkerW();
            SetTraySystem();
            FillDisplay();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            HideTraySystem();
        }
    }
}
