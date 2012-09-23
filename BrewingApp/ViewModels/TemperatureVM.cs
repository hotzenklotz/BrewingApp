using BrewingApp.Converters;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using BrewingApp.Models;

namespace BrewingApp.ViewModels
{
    public class TemperatureVM : ViewModelBase
    {
        private float _Celsius;
        private string _Temp1Selection;
        private string _Temp2Selection;

        private TemperatureConverter _Converter;

        public ObservableCollection<string> TemperatureUnits{ get; set; }

        public TemperatureVM()
        {
            this._Converter = new TemperatureConverter();
            this.TemperatureUnits = Settings.TemperatureUnits;

            Temp1Selection = TemperatureUnits[0];
            Temp2Selection = TemperatureUnits[1];
        }

        public string Temp1Selection
        {
          get { return _Temp1Selection; }
            set { _Temp1Selection = value; TemperaturePropertiesChanged(); }
        }
                
        public string Temp2Selection
        {
          get { return _Temp2Selection; }
            set { _Temp2Selection = value; TemperaturePropertiesChanged(); }
        }


        /// <summary>
        /// Stores the temperature value for the first textbox / dropdown
        /// </summary>
        public float Temp1
        {
            get { return this._Converter.ConvertBack(this._Celsius, Temp1Selection); }
            set
            {
                this._Celsius = this._Converter.Convert(value, Temp1Selection);
                TemperaturePropertiesChanged();               
            }
        }

        /// <summary>
        /// Stores the temperature value for the second textbox / dropdown
        /// </summary>
        public float Temp2
        {
            get { return this._Converter.ConvertBack(this._Celsius, Temp2Selection); }
            set
            {
                 this._Celsius= this._Converter.Convert(value, Temp2Selection);
                 TemperaturePropertiesChanged();
            }
        }


        private void TemperaturePropertiesChanged()
        {
            RaisePropertyChanged("Temp1");
            RaisePropertyChanged("Temp2");
        }
  
    }
}
