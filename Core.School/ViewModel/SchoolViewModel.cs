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

        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

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

        private List<Model.School> _enteredSchools = new List<Model.School>();
        public List<Model.School> EnteredSchools
        {
            get => _enteredSchools;
            set => SetProperty(ref _enteredSchools, value);
        }

        public delegate void LoadingIndicatorsEventHandler(object sender, bool isActive);
        public event LoadingIndicatorsEventHandler LoadingIndicatorsAction;

        public SchoolViewModel()
        {
            SetSelectedSchool();
        }

        private async void SetSelectedSchool()
        {
            LoadingIndicatorsAction?.Invoke(this, true);
            await Task.Run(() => { EnteredSchools = schoolService.LoadSchoolsInfo(EnteredSchoolName); }); 
            SetStatus();
            LoadingIndicatorsAction?.Invoke(this, false);
        }

        private void SetStatus()
        {
            if (EnteredSchools == null)
            {
                Status = "네트워크 연결을 확인해 주세요";
            }
            else if (EnteredSchools.Count == 0)
            {
                Status = "학교명을 확인해 주세요";
            }
            else
            {
                Status = "";
            }
        }
    }
}
