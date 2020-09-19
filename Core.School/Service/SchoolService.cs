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
        private string NeisSchoolApi = "https://open.neis.go.kr/hub/schoolInfo?SCHUL_NM=";

        internal List<Model.School> LoadSchoolsInfo(string schoolName)
        {
            List<Model.School> schools = new List<Model.School>();

            SetNeisSchoolApi(schoolName);

            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                wc.Encoding = Encoding.UTF8;

                string xml = wc.DownloadString(NeisSchoolApi);

                JObject jObj = JObject.Parse(xml);

                schools = SetSchools(jObj, schools);
            }
            catch
            {
                // TODO : 네트워크 오류 처리
            }

            return schools;
        }

        private List<Model.School> SetSchools(JObject jObj, List<Model.School> schools)
        {
            if (jObj["schoolInfo"] == null)
            {
                return schools;
            }

            int dataCount = Convert.ToInt32(jObj["schoolInfo"][0]["head"][0]["list_total_count"]);
            int length = (dataCount > 5) ? 5 : dataCount;

            for (int i = 0; i < length; i++)
            {
                Model.School school = new Model.School();

                school.EducationOfficeCode = jObj["mealServiceDietInfo"][1]["row"][i]["ATPT_OFCDC_SC_CODE"].ToString();
                school.SchoolCode = jObj["mealServiceDietInfo"][1]["row"][i]["SD_SCHUL_CODE"].ToString();
                school.SchoolName = jObj["mealServiceDietInfo"][1]["row"][i]["SCHUL_NM"].ToString();
                school.SchoolAddress = jObj["mealServiceDietInfo"][1]["row"][i]["ORG_RDNMA"].ToString();

                schools.Add(school);
            }

            return schools;
        }

        private void SetNeisSchoolApi(string schoolName)
        {
            NeisSchoolApi += schoolName;
        }
    }
}
