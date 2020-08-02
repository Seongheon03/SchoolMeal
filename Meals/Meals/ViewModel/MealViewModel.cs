using Meal_Parsing.Model.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Meal_Parsing.ViewModel
{
    class MealViewModel : BindableBase
    {
        // 시도교육청코드
        const string ATPT_OFCDC_SC_CODE = "D10"; // 대구광역시교육청
        // 표준학교코드
        const string SD_SCHUL_CODE = "7240393"; // 대구소프트웨어고등학교
        string NeisMealApi = "https://open.neis.go.kr/hub/mealServiceDietInfo?ATPT_OFCDC_SC_CODE=" + ATPT_OFCDC_SC_CODE + "&SD_SCHUL_CODE=" + SD_SCHUL_CODE;

        //public delegate void ScrollViewerVisibilityHandler(object sender, bool isUsable);
        //public event ScrollViewerVisibilityHandler ScrollViewerVisibility;

        #region Property
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private string _breakfast;
        public string Breakfast
        {
            get => _breakfast;
            set => SetProperty(ref _breakfast, value);
        }

        private string _lunch;
        public string Lunch
        {
            get => _lunch;
            set => SetProperty(ref _lunch, value);
        }

        private string _dinner;
        public string Dinner
        {
            get => _dinner;
            set => SetProperty(ref _dinner, value);
        }

        private Visibility _tbVisibility;
        public Visibility TbVisibility
        {
            get => _tbVisibility;
            set => SetProperty(ref _tbVisibility, value);
        }
        #endregion

        #region Delegate
        public DelegateCommand TodayCommand { get; set; }
        public DelegateCommand NextDayCommand { get; set; }
        public DelegateCommand PrevDayCommand { get; set; }
        #endregion

        public MealViewModel()
        {
            LoadMealData(DateTime.Now);
            TodayCommand = new DelegateCommand(OnToday);
            NextDayCommand = new DelegateCommand(OnNextDay);
            PrevDayCommand = new DelegateCommand(OnPrevDay);
        }

        private void OnToday()
        {
            SelectedDate = DateTime.Now;
            LoadMealData(SelectedDate);
        }

        private void OnNextDay()
        {
            SelectedDate = SelectedDate.AddDays(1);
            LoadMealData(SelectedDate);
        }

        private void OnPrevDay()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            LoadMealData(SelectedDate);
        }

        private void LoadMealData(DateTime date)
        {
            Initialize();
            setNEIS_MEAL_URL(date);

            SelectedDate = date;

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
            catch (WebException e)
            {
                MessageBox.Show("네트워크 연결을 확인해 주세요.");
                System.Windows.Application.Current.Shutdown();
            }

        }

        private void Initialize()
        {
            Breakfast = "오늘 조식은 없습니다.";
            Lunch = "오늘 중식은 없습니다.";
            Dinner = "오늘 석식은 없습니다.";

            //TbVisibility = Visibility.Visible;
            //ScrollViewerVisibility?.Invoke(this, true);
        }

        private void setNEIS_MEAL_URL(DateTime date)
        {
            NeisMealApi += "&MLSV_YMD=" + date.ToString("yyyyMMdd");
        }

        private void SetSelectedMeal(JObject jobj)
        {
            if (jobj["mealServiceDietInfo"] == null)
            {
                Breakfast = "오늘은 급식이 없습니다.";
                Lunch = "오늘은 급식이 없습니다.";
                Dinner = "오늘은 급식이 없습니다.";

                //TbVisibility = Visibility.Hidden;
                //ScrollViewerVisibility?.Invoke(this, false);

                return;
            }

            for (int i = 0; i < Convert.ToInt32(jobj["mealServiceDietInfo"][0]["head"][0]["list_total_count"]); i++)
            {
                string selectedMeal = jobj["mealServiceDietInfo"][1]["row"][i]["DDISH_NM"].ToString();

                switch (jobj["mealServiceDietInfo"][1]["row"][i]["MMEAL_SC_NM"].ToString())
                {
                    case "조식":
                        Breakfast = ParseString(selectedMeal);
                        break;
                    case "중식":
                        Lunch = ParseString(selectedMeal);
                        break;
                    case "석식":
                        Dinner = ParseString(selectedMeal);
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
