using Core.School.Model;
using SchoolMeal.Properties;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMeal.Control.Setting
{
    /// <summary>
    /// Interaction logic for SettingSchoolControl.xaml
    /// </summary>
    public partial class SettingSchoolControl : UserControl
    {
        public SettingSchoolControl()
        {
            InitializeComponent();
            Loaded += SettingSchoolControl_Loaded;
            Unloaded += SettingSchoolControl_Unloaded;
        }

        private void SettingSchoolControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.schoolViewModel;
            App.schoolViewModel.LoadingIndicatorsAction += SchoolViewModel_LoadingIndicatorsAction;
        }

        private void SchoolViewModel_LoadingIndicatorsAction(object sender, bool isActive)
        {
            progressRing.IsActive = isActive;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            School SelectedSchool = (School)lbSchools.SelectedItem;

            if (SelectedSchool != null)
            {
                App.mealViewModel.InitMealData(SelectedSchool.EducationOfficeCode, SelectedSchool.SchoolCode);

                Settings.Default.currentSchool.EducationOfficeCode = SelectedSchool.EducationOfficeCode;
                Settings.Default.currentSchool.SchoolCode = SelectedSchool.SchoolCode;
                Settings.Default.currentSchool.SchoolName = SelectedSchool.SchoolName;
            }
        }

        private void SettingSchoolControl_Unloaded(object sender, RoutedEventArgs e)
        {
            App.schoolViewModel.EnteredSchoolName = "";
        }
    }
}
