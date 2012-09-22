using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using BrewingApp.Converters;
using BrewingApp.Models;

namespace BrewingApp.ViewModels
{
    public class VolumeVM : ViewModelBase
    {

        public ObservableCollection<string> USUnitList;
        public ObservableCollection<string> MetricUnitList;


        private float _liter;
        private string _USUnit;
        private string _MetricUnit;

        private VolumeConverter _Converter;

        public VolumeVM()
        {
            this._Converter = new VolumeConverter();
            MetricUnitList = Settings.MetricVolumeUnits;
            USUnitList = Settings.USVolumeUnits;

        }

        public string USUnit
        {
            get
            {
                return this._USUnit;
            }

            set
            {
                _USUnit = value;
                FluidPropertiesChanged();
            }
        }

        public string MetricUnit
        {
            get { return this._MetricUnit; }
            set
            {
                this._MetricUnit = value;
                FluidPropertiesChanged();
            }
        }

        public float USFluid
        {
            get { return UnitConverter.MetricToUSFluid(this._liter, this.USUnit); }
            set
            {
                float tmpLiter = UnitConverter.USToMetricFluid(value, this.USUnit);
                if (MetricFluid != tmpLiter)
                    this._liter = tmpLiter;
                FluidPropertiesChanged();
            }
        }

        public float MetricFluid
        {
            get { return UnitConverter.LiterToMetric(this._liter, this.MetricUnit); }
            set
            {
                this._liter = UnitConverter.MetricToLiter(value, this.MetricUnit);
                FluidPropertiesChanged();
            }
        }

        private void FluidPropertiesChanged()
        {
            RaisePropertyChanged("USFluid");
            RaisePropertyChanged("MetricFluid");
        }
    
    }
}
