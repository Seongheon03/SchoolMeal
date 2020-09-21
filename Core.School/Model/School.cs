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
        public string EducationOfficeCode { get; set; }

        public string SchoolCode { get; set; }

        private string _schoolName;
        public string SchoolName
        {
            get => _schoolName;
            set => SetProperty(ref _schoolName, value);
        }

        public string SchoolAddress { get; set; }

        public School()
        {
            SchoolName = "대구소프트웨어고등학교";
            EducationOfficeCode = "D10";
            SchoolCode = "7240393";
        }
    }
}
