using Core.School.Service;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.School.ViewModel
{
    public class SchoolViewModel : BindableBase
    {
        private SchoolService schoolService = new SchoolService();

        private string _enteredSchool;
        public string EnteredSchool
        {
            get => _enteredSchool;
            set
            {
                SetProperty(ref _enteredSchool, value);
                SetSelectedSchool();
            }
        }

        private Model.School _selectedSchool;
        public Model.School SelectedSchool
        {
            get => _selectedSchool;
            set => SetProperty(ref _selectedSchool, value);
        }

        public delegate void LoadCompleteEventHandler(object sender, bool success);
        public event LoadCompleteEventHandler CompleteAction;

        private void SetSelectedSchool()
        {
            CompleteAction?.Invoke(this, false);
            schoolService.LoadSchoolsInfo(EnteredSchool);
            CompleteAction?.Invoke(this, true);
        }
    }
}
