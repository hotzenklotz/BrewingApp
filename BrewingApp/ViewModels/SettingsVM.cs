using System.Collections.ObjectModel;
using BrewingApp.Models;
using GalaSoft.MvvmLight;
using Microsoft.Phone.Shell;
using BrewingApp.Other;

namespace BrewingApp.ViewModels
{
    public class SettingsVM : ViewModelBase
    {
        private string _WeightSelection;
        private string _VolumeSelection;
        private string _TemperatureSelection;
        private string _HopFormulaSelection;

        private ShellToast _Toast = new ShellToast();

        public ObservableCollection<string> HopFormulaList { get; set; }

        public ObservableCollection<string> WeightUnitList { get; set; }
        public ObservableCollection<string> VolumeUnitList { get; set; }
        public ObservableCollection<string> TemperatureUnitList { get; set; }

        public SettingsVM()
        {
            TemperatureUnitList = Settings.TemperatureUnits;
            HopFormulaList = Settings.HopFormulas;
            
            //add both unit lists, the metric and american unit, to the listpicker
            WeightUnitList = new ObservableCollection<string>();
            WeightUnitList.AddRange(Settings.MetricWeightUnits);
            WeightUnitList.AddRange(Settings.USWeightUnits);


            //same here for volume units
            VolumeUnitList = new ObservableCollection<string>();
            VolumeUnitList.AddRange(Settings.MetricVolumeUnits);
            VolumeUnitList.AddRange(Settings.USVolumeUnits);


            //selected items
            WeightSelection = Settings.WeightUnit;
            VolumeSelection = Settings.VolumeUnit;
            TemperatureSelection = Settings.TemperatureUnit;
            HopFormulaSelection = Settings.HopFormula;

        }

        public string WeightSelection 
        {
            get { return this._WeightSelection; }
            set 
            { 
                this._WeightSelection = value;
                UpdateSettings();
            }
        }

        public string VolumeSelection 
        { 
            get { return this._VolumeSelection; }
            set 
            {
                this._VolumeSelection = value;
                UpdateSettings();
            }
        }

        public string TemperatureSelection
        {
            get { return this._TemperatureSelection; }
            set
            {
                this._TemperatureSelection = value;
                UpdateSettings();
            }
        }

        public string HopFormulaSelection
        {
            get { return this._HopFormulaSelection; }
            set 
            { 
                this._HopFormulaSelection = value;
                UpdateSettings();
            }
        }

        private void UpdateSettings()
        {
            Settings.VolumeUnit = this._VolumeSelection;
            Settings.TemperatureUnit = this._TemperatureSelection;
            Settings.HopFormula = this._HopFormulaSelection;
            Settings.WeightUnit = this._WeightSelection;

            Settings.Save();

            this._Toast.Title = "BrewingApp";
            this._Toast.Content = "Please restart the app for the changes to take effect!";
            this._Toast.Show();
        }
    }
}
