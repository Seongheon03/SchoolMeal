using SchoolMeal.Common;
using SchoolMeal.Properties;
using Prism.Mvvm;
using System;
using System.Windows.Media;

namespace SchoolMeal.ViewModel
{
    public class SettingViewModel : BindableBase
    {
        private Color _backgroundColor = Setting.backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        private Color _fontColor = Setting.fontColor;
        public Color FontColor
        {
            get => _fontColor;
            set => SetProperty(ref _fontColor, value);
        }

        private bool _isWindowVisible = true;
        public bool IsWindowVisible
        {
            get => _isWindowVisible;
            set => SetProperty(ref _isWindowVisible, value);
        }

        private bool _isStartingProgram = Setting.isStartingProgram;
        public bool IsStartingProgram
        {
            get => _isStartingProgram;
            set
            {
                SetProperty(ref _isStartingProgram, value);

                if (IsStartingProgram)
                {
                    App.RunRegKey.SetValue(App.SystemName, Environment.CurrentDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName);
                }
                else
                {
                    App.RunRegKey.SetValue(App.SystemName, false);
                }

            }
        }
    }
}
