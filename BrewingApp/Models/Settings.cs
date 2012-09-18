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

namespace BrewingApp.Models
{
    public class Settings
    {

        private string _VolumeUnit;
        private string _WeightUnit;
        private string _TemperatureUnit;
        private string _IBUFormula;

        private IsolatedStorageSettings _UserSettings;

        public Settings()
        {
            this._UserSettings = IsolatedStorageSettings.ApplicationSettings;

            if (this._UserSettings.Count == 0)
            {
                SetDefaultValues();
            } else {
                LoadValues();
            }       
        }

        private void LoadValues()
        {
            this._TemperatureUnit = (string) this._UserSettings["TemperatureUnit"];
            this._WeightUnit = (string) this._UserSettings["WeightUnit"];
            this._VolumeUnit = (string) this._UserSettings["VolumeUnit"];
            this._IBUFormula = (string)this._UserSettings["IBUFormula"];
        }

        private void SetDefaultValues()
        {
            this._TemperatureUnit = "Celsius";
            this._WeightUnit = "Gramm";
            this._VolumeUnit = "Liter";
            this._IBUFormula = "Rager";

            this._UserSettings.Add("TemperatureUnit", this._TemperatureUnit);
            this._UserSettings.Add("WeightUnit", this._WeightUnit);
            this._UserSettings.Add("VolumeUnit", this._VolumeUnit);
            this._UserSettings.Add("IBUFormula", this._IBUFormula);
        }

        public void Save()
        {
            this._UserSettings.Save();
        }

    }
}
