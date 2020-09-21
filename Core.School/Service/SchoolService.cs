using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.School.Service
{
    public class SchoolService
    {
        private const string NEIS_SCHOOL_URL = "https://open.neis.go.kr/hub/schoolInfo?KEY=2ba290736ae3424aa5c5d2444b76dac3&SCHUL_NM=";
        private string neisSchoolUrl;

        internal List<Model.School> LoadSchoolsInfo(string schoolName)
        {
            List<Model.School> schools = new List<Model.School>();

            SetNeisSchoolUrl(schoolName);

            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                wc.Encoding = Encoding.UTF8;

                string xml = wc.DownloadString(neisSchoolUrl);

                JObject jObj = JObject.Parse(xml);

                schools = SetSchools(jObj, schools);
            }
            catch
            {
                return null;
            }

           return schools;
        }

        private void SetNeisSchoolUrl(string schoolName)
        {
            neisSchoolUrl = NEIS_SCHOOL_URL + schoolName;
        }

        private List<Model.School> SetSchools(JObject jObj, List<Model.School> schools)
        {
            if (jObj["schoolInfo"] == null)
            {
                return schools;
            }

            int dataCount = Convert.ToInt32(jObj["schoolInfo"][0]["head"][0]["list_total_count"]);
            int length = (dataCount > 100) ? 100 : dataCount;

            for (int i = 0; i < length; i++)
            {
                Model.School school = new Model.School();

                school.EducationOfficeCode = jObj["schoolInfo"][1]["row"][i]["ATPT_OFCDC_SC_CODE"].ToString();
                school.SchoolCode = jObj["schoolInfo"][1]["row"][i]["SD_SCHUL_CODE"].ToString();
                school.SchoolName = jObj["schoolInfo"][1]["row"][i]["SCHUL_NM"].ToString();
                school.SchoolAddress = jObj["schoolInfo"][1]["row"][i]["ORG_RDNMA"].ToString();

                schools.Add(school);
            }

            return schools;
        }
    }
}
