using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Meal.Service
{
    internal class MealService
    {
        private string NeisMealApi;

        public void SetBasicNeisMealApi(string educationCode, string schoolCode)
        {
            NeisMealApi = "https://open.neis.go.kr/hub/mealServiceDietInfo?ATPT_OFCDC_SC_CODE=" + educationCode + "&SD_SCHUL_CODE=" + schoolCode;
        }

        internal Model.Meal LoadMealData(DateTime date)
        {
            Model.Meal meal = new Model.Meal();
            InitMeals(meal);

            SetNeisMealApi(date);

            WebClient wc = new WebClient();
            wc.Headers["Content-type"] = "application/json";
            wc.Encoding = Encoding.UTF8;

            try
            {
                string xml = wc.DownloadString(NeisMealApi);

                JObject todayMeal = JObject.Parse(xml);

                meal = SetMeal(todayMeal, meal);
            }
            catch
            {
                meal.Breakfast = "네트워크 연결을 확인해주세요.";
                meal.Lunch = "네트워크 연결을 확인해주세요.";
                meal.Dinner = "네트워크 연결을 확인해주세요.";
            }

            return meal;
        }

        private void InitMeals(Model.Meal meals)
        {
            meals.Breakfast = "오늘은 조식이 없습니다.";
            meals.Lunch = "오늘은 중식이 없습니다.";
            meals.Dinner = "오늘은 석식이 없습니다.";
        }

        private void SetNeisMealApi(DateTime date)
        {
            NeisMealApi += "&MLSV_YMD=" + date.ToString("yyyyMMdd");
        }

        private Model.Meal SetMeal(JObject jobj, Model.Meal meals)
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
            str = Regex.Replace(str, @"[a-zA-Z0-9]", "");
            str = str.Replace(".", "");
            str = str.Replace("</>", ", ");

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
