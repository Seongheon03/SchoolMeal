using Prism.Mvvm;

namespace Core.Meal.Model
{
    public class Meal : BindableBase
    {
        private bool _isExist;
        public bool IsExist
        {
            get => _isExist;
            set => SetProperty(ref _isExist, value);
        }

        private string _breakfast;
        public string Breakfast
        {
            get => _breakfast;
            set => SetProperty(ref _breakfast, value);
        }

        private string _lunch;
        public string Lunch
        {
            get => _lunch;
            set => SetProperty(ref _lunch, value);
        }

        private string _dinner;
        public string Dinner
        {
            get => _dinner;
            set => SetProperty(ref _dinner, value);
        }
    }
}
