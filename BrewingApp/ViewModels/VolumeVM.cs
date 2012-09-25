using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using BrewingApp.Converters;
using BrewingApp.Models;

namespace BrewingApp.ViewModels
{
    public class VolumeVM : ViewModelBase
    {

        public ObservableCollection<string> USUnitList { get; set; }
        public ObservableCollection<string> MetricUnitList{ get; set; }

        private string _Unit1Selection;
        private string _Unit2Selection;

        private float _liter;

        private VolumeConverter _Converter;

        public VolumeVM()
        {
            this._Converter = new VolumeConverter();

            MetricUnitList = Settings.MetricVolumeUnits;
            USUnitList = Settings.USVolumeUnits;

            Unit1Selection = MetricUnitList[0];
            Unit2Selection = USUnitList[0];
            
        }

        public string Unit1Selection
        {
            get { return _Unit1Selection; }
            set { _Unit1Selection = value; VolumePropertiesChanged(); }
        }

        public string Unit2Selection
        {
            get { return _Unit2Selection; }
            set { _Unit2Selection = value; VolumePropertiesChanged();  }
        }

        public float Unit1
        {
            get { return this._Converter.ConvertBack(this._liter, Unit1Selection); }
            set
            {
                this._liter = this._Converter.Convert(value, Unit1Selection);
                VolumePropertiesChanged();
            }
        }

        public float Unit2
        {
            get { return this._Converter.Convert(this._liter, Unit2Selection); }
            set
            {
                this._liter = this._Converter.ConvertBack(value, Unit2Selection);
                VolumePropertiesChanged();
            }
        }

        private void VolumePropertiesChanged()
        {
            RaisePropertyChanged("Unit1");
            RaisePropertyChanged("Unit2");
        }
    
    }
}
