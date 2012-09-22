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

        public string Unit1Selection;
        public string Unit2Selection;


        private float _liter;
        private string _Unit1;
        private string _Unit2;

        private VolumeConverter _Converter;

        public VolumeVM()
        {
            this._Converter = new VolumeConverter();

            MetricUnitList = Settings.MetricVolumeUnits;
            USUnitList = Settings.USVolumeUnits;

            Unit1Selection = MetricUnitList.

        }

        public float Unit1
        {
            get { return this._Converter.ConvertBack(this._liter, Unit1Selection); }
            set
            {
                float tmpLiter = this._Converter.Convert(value, Unit1Selection);
                //if (MetricFluid != tmpLiter)
                    this._liter = tmpLiter;
                FluidPropertiesChanged();
            }
        }

        public float Unit2
        {
            get { return this._Converter.ConvertBack(this._liter, this.Unit2Selection); }
            set
            {
                this._liter = this._Converter.Convert(value, this.Unit2Selection);
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
