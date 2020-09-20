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

        private string _enteredSchoolName;
        public string EnteredSchoolName
        {
            get => _enteredSchoolName;
            set
            {
                SetProperty(ref _enteredSchoolName, value);
                SetSelectedSchool();
            }
        }

        private List<Model.School> _enteredSchools;
        public List<Model.School> EnteredSchools
        {
            get => _enteredSchools;
            set => SetProperty(ref _enteredSchools, value);
        }

        public delegate void LoadCompleteEventHandler(object sender, bool success);
        public event LoadCompleteEventHandler CompleteAction;

        private void SetSelectedSchool()
        {
            CompleteAction?.Invoke(this, false);
            EnteredSchools = schoolService.LoadSchoolsInfo(EnteredSchoolName);
            CompleteAction?.Invoke(this, true);
        }
    }
}
