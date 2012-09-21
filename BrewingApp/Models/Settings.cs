using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace BrewingApp.Models
{
    public static class Settings
    {

        public static string VolumeUnit;
        public static string WeightUnit;
        public static string TemperatureUnit;
        private static string IBUFormula;

        public static IsolatedStorageSettings UserSettings;

        public static ObservableCollection<string> TemperatureUnits = new ObservableCollection<string>
        {
            "Celsius",
            "Fahrenheit"
        };

        public static ObservableCollection<string> USVolumeUnits = new ObservableCollection<string>() 
        {
            "US Gallon",
            "US Ounce"

        };

        public static ObservableCollection<string> MetricVolumeUnits = new ObservableCollection<string>() 
        {
            "Liter",
            "Milliliter"

        };



        public static void Initialize()
        {
            UserSettings = IsolatedStorageSettings.ApplicationSettings;

            if (UserSettings.Count == 0)
            {
                SetDefaultValues();
            } else {
                LoadValues();
            }       
        }

        private static void LoadValues()
        {
            TemperatureUnit = (string) UserSettings["TemperatureUnit"];
            WeightUnit = (string) UserSettings["WeightUnit"];
            VolumeUnit = (string) UserSettings["VolumeUnit"];
            IBUFormula = (string)UserSettings["IBUFormula"];
        }

        private static void SetDefaultValues()
        {
            TemperatureUnit = "Celsius";
            WeightUnit = "Gramm";
            VolumeUnit = "Liter";
            IBUFormula = "Rager";

            UserSettings.Add("TemperatureUnit", TemperatureUnit);
            UserSettings.Add("WeightUnit", WeightUnit);
            UserSettings.Add("VolumeUnit", VolumeUnit);
            UserSettings.Add("IBUFormula", IBUFormula);
        }

        public void Save()
        {
            UserSettings.Save();
        }

    }
}
