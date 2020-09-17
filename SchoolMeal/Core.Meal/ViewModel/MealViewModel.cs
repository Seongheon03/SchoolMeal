using Core.Meal.Model;
using Core.Meal.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Core.Meal.ViewModel
{
    public class MealViewModel : BindableBase
    {
        private MealService mealService = new MealService();

        //public delegate void ScrollViewerVisibilityHandler(object sender, bool isUsable);
        //public event ScrollViewerVisibilityHandler ScrollViewerVisibility;

        #region Property
        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private Meals _todayMeal;
        public Meals TodayMeal
        {
            get => _todayMeal;
            set => SetProperty(ref _todayMeal, value);
        }
        #endregion

        #region Delegate
        public DelegateCommand PrevDayCommand { get; set; }
        public DelegateCommand TodayCommand { get; set; }
        public DelegateCommand NextDayCommand { get; set; }
        #endregion

        public MealViewModel()
        {
            LoadMealData(SelectedDate);
            PrevDayCommand = new DelegateCommand(OnPrevDay);
            TodayCommand = new DelegateCommand(OnToday);
            NextDayCommand = new DelegateCommand(OnNextDay);
        }

        private void OnPrevDay()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            LoadMealData(SelectedDate);
        }

        private void OnToday()
        {
            SelectedDate = DateTime.Now;
            LoadMealData(SelectedDate);
        }

        private void OnNextDay()
        {
            SelectedDate = SelectedDate.AddDays(1);
            LoadMealData(SelectedDate);
        }

        public void LoadMealData(DateTime date)
        {
            TodayMeal = mealService.LoadMealData(date);
        }
    }
}
