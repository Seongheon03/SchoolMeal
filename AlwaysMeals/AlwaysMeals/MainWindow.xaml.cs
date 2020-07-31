using Meal_Parsing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AlwaysMeals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MealViewModel mealViewModel = new MealViewModel();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = mealViewModel;
            //mealViewModel.ScrollViewerVisibility += MealViewModel_ScrollViewerVisibility;
        }

        //private void MealViewModel_ScrollViewerVisibility(object sender, bool isUsable)
        //{
        //    if (isUsable)
        //    {
        //        svMeal.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        //    }
        //    else
        //    {
        //        svMeal.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        //    }
        //}
    }
}
