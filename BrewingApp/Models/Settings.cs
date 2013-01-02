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

namespace BrewBuddy.Models
{
    public class Settings
    {

        public static string VolumeUnit;
        public static string WeightUnit;
        public static string TemperatureUnit;
        public static string HopFormula;

        public static IsolatedStorageSettings UserSettings;

        public static ObservableCollection<string> TemperatureUnits = new ObservableCollection<string>()
        {
            "Celsius",
            "Fahrenheit"
        };

        public static ObservableCollection<string> USVolumeUnits = new ObservableCollection<string>() 
        {
            "US Gallon",
            "US Ounce",
            "US Quart"

        };

        public static ObservableCollection<string> MetricVolumeUnits = new ObservableCollection<string>() 
        {
            "Liter",
            "Milliliter"

        };

        public static ObservableCollection<string> MetricWeightUnits = new ObservableCollection<string>()
        {
            "Gram",
            "Kilogram"
        };

        public static ObservableCollection<string> USWeightUnits = new ObservableCollection<string>()
        {
            "Ounce",
            "Pound"
        };

        public static ObservableCollection<string> HopFormulas = new ObservableCollection<string>()
        {
            "Rager",
            "Tinseth"
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
            HopFormula = (string) UserSettings["HopFormula"];
        }

        private static void SetDefaultValues()
        {
            TemperatureUnit = "Celsius";
            WeightUnit = "Gram";
            VolumeUnit = "Liter";
            HopFormula = "Rager";

            UserSettings.Add("TemperatureUnit", TemperatureUnit);
            UserSettings.Add("WeightUnit", WeightUnit);
            UserSettings.Add("VolumeUnit", VolumeUnit);
            UserSettings.Add("HopFormula", HopFormula);
        }

        public static void Save()
        {

            UserSettings["TemperatureUnit"] = TemperatureUnit;
            UserSettings["WeightUnit"] = WeightUnit;
            UserSettings["VolumeUnit"] = VolumeUnit;
            UserSettings["HopFormula"] = HopFormula;

            UserSettings.Save();
        }

    }
}
