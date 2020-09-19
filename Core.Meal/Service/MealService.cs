using Core.Meal.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Meal.Service
{
    internal class MealService
    {
        // 시도교육청코드
        const string ATPT_OFCDC_SC_CODE = "D10"; // 대구광역시교육청
        // 표준학교코드
        const string SD_SCHUL_CODE = "7240393"; // 대구소프트웨어고등학교
        private string NeisMealApi = "https://open.neis.go.kr/hub/mealServiceDietInfo?ATPT_OFCDC_SC_CODE=" + ATPT_OFCDC_SC_CODE + "&SD_SCHUL_CODE=" + SD_SCHUL_CODE;

        internal Meals LoadMealData(DateTime date)
        {
            Meals meals = new Meals();
            InitMeals(meals);

            SetNEIS_MEAL_URL(date);

            WebClient wc = new WebClient();
            wc.Headers["Content-type"] = "application/json";
            wc.Encoding = Encoding.UTF8;

            try
            {
                string html = wc.DownloadString(NeisMealApi);

                JObject todayMeal = JObject.Parse(html);

                meals = SetSelectedMeal(todayMeal, meals);
            }
            catch
            {
                meals.Breakfast = "네트워크 연결을 확인해주세요.";
                meals.Lunch = "네트워크 연결을 확인해주세요.";
                meals.Dinner = "네트워크 연결을 확인해주세요.";
            }

            return meals;
        }

        private void InitMeals(Meals meals)
        {
            meals.Breakfast = "오늘은 조식이 없습니다.";
            meals.Lunch = "오늘은 중식이 없습니다.";
            meals.Dinner = "오늘은 석식이 없습니다.";
        }

        private void SetNEIS_MEAL_URL(DateTime date)
        {
            NeisMealApi += "&MLSV_YMD=" + date.ToString("yyyyMMdd");
        }

        private Meals SetSelectedMeal(JObject jobj, Meals meals)
        {
            if (jobj["mealServiceDietInfo"] == null)
            {
                meals.Breakfast = "오늘은 급식이 없습니다.";
                meals.Lunch = "오늘은 급식이 없습니다.";
                meals.Dinner = "오늘은 급식이 없습니다.";

                return meals;
            }

            for (int i = 0; i < Convert.ToInt32(jobj["mealServiceDietInfo"][0]["head"][0]["list_total_count"]); i++)
            {
                string selectedMeal = jobj["mealServiceDietInfo"][1]["row"][i]["DDISH_NM"].ToString();

                switch (jobj["mealServiceDietInfo"][1]["row"][i]["MMEAL_SC_NM"].ToString())
                {
                    case "조식":
                        meals.Breakfast = ParseString(selectedMeal);
                        break;
                    case "중식":
                        meals.Lunch = ParseString(selectedMeal);
                        break;
                    case "석식":
                        meals.Dinner = ParseString(selectedMeal);
                        break;
                }
            }

            return meals;
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
