using Meals.Properties;

namespace Meals.Common
{
    public class Setting
    {
        public static bool isStartingProgram { get; set; }
        public static System.Windows.Media.Color backgroundColor { get; set; }
        public static System.Windows.Media.Color fontColor { get; set; }

        public static void Load()
        {
            isStartingProgram = Settings.Default.isStartingProgram;

            backgroundColor =
                System.Windows.Media.Color.FromArgb
                (
                    Settings.Default.backgroundColor.A,
                    Settings.Default.backgroundColor.R,
                    Settings.Default.backgroundColor.G,
                    Settings.Default.backgroundColor.B
                );

            fontColor =
                System.Windows.Media.Color.FromArgb
                (
                    Settings.Default.fontColor.A,
                    Settings.Default.fontColor.R,
                    Settings.Default.fontColor.G,
                    Settings.Default.fontColor.B
                );
        }

        public static void Save()
        {
            Settings.Default.isStartingProgram = isStartingProgram;

            Settings.Default.backgroundColor =
                System.Drawing.Color.FromArgb
                (
                    backgroundColor.A,
                    backgroundColor.R,
                    backgroundColor.G,
                    backgroundColor.B
                );

            Settings.Default.fontColor =
                System.Drawing.Color.FromArgb
                (
                    fontColor.A,
                    fontColor.R,
                    fontColor.G,
                    fontColor.B
                );

            Settings.Default.Save();
        }
    }
}
