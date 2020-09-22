using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolMeal.Control.Setting
{
    /// <summary>
    /// Interaction logic for SettingHomeControl.xaml
    /// </summary>
    public partial class SettingHomeControl : UserControl
    {
        public SettingHomeControl()
        {
            InitializeComponent();
            Loaded += SettingHomeControl_Loaded;
        }

        private void SettingHomeControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.mealViewModel;
        }

        private void tbtnStartingProgram_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbtnStartingProgram.Status)
            {
                App.runRegKey.SetValue(App.curAssembly.GetName().Name, App.curAssembly.Location);
            }
            else
            {
                App.runRegKey.SetValue(App.curAssembly.GetName().Name, false);
            }
        }
    }
}
