using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Meals.Control
{
    /// <summary>
    /// Interaction logic for ToggleButtonControl.xaml
    /// </summary>
    public partial class ToggleButtonControl : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush CheckedBackground = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        SolidColorBrush UnCheckedBackground = new SolidColorBrush(Color.FromRgb(160, 160, 160));

        public static DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(bool), typeof(ToggleButtonControl), new PropertyMetadata(StatusChanged));

        public bool Status
        {
            get => (bool)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        private static void StatusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleButtonControl toggleButton = sender as ToggleButtonControl;
            toggleButton.SetToggledButton();
        }

        public ToggleButtonControl()
        {
            InitializeComponent();
            SetToggledButton();
        }

        private void ToggleButton_Click(object sender, MouseButtonEventArgs e)
        {
            Status = !Status;
        }

        public void SetToggledButton()
        {
            if (Status)
            {
                Back.Fill = CheckedBackground;
                Dot.Margin = RightSide;
                tbStatus.Text = "On";
            }
            else
            {
                Back.Fill = UnCheckedBackground;
                Dot.Margin = LeftSide;
                tbStatus.Text = "Off";
            }
        }
    }
}
