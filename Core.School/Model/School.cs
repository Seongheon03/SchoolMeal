using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.School.Model
{
    public class School : BindableBase
    {
        private string _educationOfficeCode;
        public string EducationOfficeCode
        {
            get => _educationOfficeCode;
            set => SetProperty(ref _educationOfficeCode, value);
        }

        private string _schoolCode;
        public string SchoolCode
        {
            get => _schoolCode;
            set => SetProperty(ref _schoolCode, value);
        }

        private string _schoolName;
        public string SchoolName
        {
            get => _schoolName;
            set => SetProperty(ref _schoolName, value);
        }

        private string _schoolAddress;
        public string SchoolAddress
        {
            get => _schoolAddress;
            set => SetProperty(ref _schoolAddress, value);
        }
    }
}
