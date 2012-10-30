using System.Collections.ObjectModel;
using BrewingApp.Models;
using GalaSoft.MvvmLight;
using BrewingApp.Other;

namespace BrewingApp.ViewModels
{
    public class SettingsVM : ViewModelBase
    {
        private string _WeightSelection;
        private string _VolumeSelection;
        private string _TemperatureSelection;
        private string _HopFormulaSelection;

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
            this._WeightSelection = Settings.WeightUnit;
            this._VolumeSelection = Settings.VolumeUnit;
            this._TemperatureSelection = Settings.TemperatureUnit;
            this._HopFormulaSelection = Settings.HopFormula;

            RaisePropertyChanged("WeightSelection");
            RaisePropertyChanged("TemperatureSelection");
            RaisePropertyChanged("VolumeSelection");
            RaisePropertyChanged("HopFormula");

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
        }
    }
}
