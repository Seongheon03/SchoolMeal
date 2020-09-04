using Core.SchoolMeal.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.SchoolMeal.Service
{
    internal class MealService
    {
        // 시도교육청코드
        const string ATPT_OFCDC_SC_CODE = "D10"; // 대구광역시교육청
        // 표준학교코드
        const string SD_SCHUL_CODE = "7240393"; // 대구소프트웨어고등학교
        string NeisMealApi = "https://open.neis.go.kr/hub/mealServiceDietInfo?ATPT_OFCDC_SC_CODE=" + ATPT_OFCDC_SC_CODE + "&SD_SCHUL_CODE=" + SD_SCHUL_CODE;

        Meal Meals = new Meal();

        internal Meal LoadMealData(DateTime date)
        { 
            InitMeals();

            SetNEIS_MEAL_URL(date);

            WebClient wc = new WebClient();
            wc.Headers["Content-type"] = "application/json";
            wc.Encoding = Encoding.UTF8;

            try
            {
                string html = wc.DownloadString(NeisMealApi);
                // HtmlAgilityPack 패키지
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                string json = doc.Text;

                JObject todayMeal = JObject.Parse(json);

                SetSelectedMeal(todayMeal);
            }
            catch
            {
                Meals.Breakfast = "네트워크 연결을 확인해주세요.";
                Meals.Lunch = "네트워크 연결을 확인해주세요.";
                Meals.Dinner = "네트워크 연결을 확인해주세요.";
            }

            return Meals;
        }

        private void InitMeals()
        {
            Meals.Breakfast = "오늘은 조식이 없습니다.";
            Meals.Lunch = "오늘은 중식이 없습니다.";
            Meals.Dinner = "오늘은 석식이 없습니다.";
        }

        private void SetNEIS_MEAL_URL(DateTime date)
        {
            NeisMealApi += "&MLSV_YMD=" + date.ToString("yyyyMMdd");
        }

        private void SetSelectedMeal(JObject jobj)
        {
            if (jobj["mealServiceDietInfo"] == null)
            {
                Meals.Breakfast = "오늘은 급식이 없습니다.";
                Meals.Lunch = "오늘은 급식이 없습니다.";
                Meals.Dinner = "오늘은 급식이 없습니다.";

                return;
            }

            for (int i = 0; i < Convert.ToInt32(jobj["mealServiceDietInfo"][0]["head"][0]["list_total_count"]); i++)
            {
                string selectedMeal = jobj["mealServiceDietInfo"][1]["row"][i]["DDISH_NM"].ToString();

                switch (jobj["mealServiceDietInfo"][1]["row"][i]["MMEAL_SC_NM"].ToString())
                {
                    case "조식":
                        Meals.Breakfast = ParseString(selectedMeal);
                        break;
                    case "중식":
                        Meals.Lunch = ParseString(selectedMeal);
                        break;
                    case "석식":
                        Meals.Dinner = ParseString(selectedMeal);
                        break;
                }
            }
        }


        private string ParseString(string str)
        {
            str = Regex.Replace(str, @"\d", "");
            str = str.Replace(".", "");
            str = str.Replace("<br/>", ", ");

            while (str.Contains("("))
            {
                //int start = str.IndexOf('(');
                //int finish = str.IndexOf(')');
                //str = str.Remove(start, finish - start + 1);

                str = str.Remove(str.IndexOf('('), str.IndexOf(')') - str.IndexOf('(') + 1);
            }

            return str;
        }
    }
}
