using SchoolMeal.Properties;
using System;
using System.Windows;

namespace SchoolMeal.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SettingWidget();
            SetTraySystem();

            DataContext = App.mealViewModel;
            App.mealViewModel.InitMealData(Settings.Default.currentSchool.EducationOfficeCode, Settings.Default.currentSchool.SchoolCode);
        }

        private void SettingWidget()
        {
            InitWorkerW();
            InitOnDisplaySettingChanged();
            ShowOnWorkerW();
            FillDisplay();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            HideTraySystem();
        }
    }
}
