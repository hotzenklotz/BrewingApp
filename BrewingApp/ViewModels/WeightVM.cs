using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using BrewingApp.Models;
using BrewingApp.Converters;
using System.Collections.ObjectModel;

namespace BrewingApp.ViewModels
{
    public class WeightVM : ViewModelBase
    {
        public ObservableCollection<string> USUnitList { get; set; }
        public ObservableCollection<string> MetricUnitList{ get; set; }

        private string _Unit1Selection;
        private string _Unit2Selection;

        private float _Gram;

        private WeightConverter _Converter;

        public WeightVM()
        {
            this._Converter = new WeightConverter();

            MetricUnitList = Settings.MetricWeightUnits;
            USUnitList = Settings.USWeightUnits;

            Unit1Selection = MetricUnitList[0];
            Unit2Selection = USUnitList[0];
            
        }

        public string Unit1Selection
        {
            get { return _Unit1Selection; }
            set { _Unit1Selection = value; WeightPropertiesChanged(); }
        }

        public string Unit2Selection
        {
            get { return _Unit2Selection; }
            set { _Unit2Selection = value; WeightPropertiesChanged();  }
        }

        public float Unit1
        {
            get { return this._Converter.ConvertBack(this._Gram, Unit1Selection); }
            set
            {
                this._Gram = this._Converter.Convert(value, Unit1Selection);
                WeightPropertiesChanged();
            }
        }

        public float Unit2
        {
            get { return this._Converter.Convert(this._Gram, this.Unit2Selection); }
            set
            {
                this._Gram = this._Converter.ConvertBack(value, this.Unit2Selection);
                WeightPropertiesChanged();
            }
        }

        private void WeightPropertiesChanged()
        {
            RaisePropertyChanged("Unit1");
            RaisePropertyChanged("Unit2");
        }
    
    }
    
}
