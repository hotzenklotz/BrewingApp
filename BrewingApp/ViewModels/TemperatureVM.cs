using BrewingApp.Converters;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using BrewingApp.Models;

namespace BrewingApp.ViewModels
{
    public class TemperatureVM : ViewModelBase
    {
        private float _Celsius;
        private TemperatureConverter _Converter;
        public ObservableCollection<string> TemperatureUnits;

        public TemperatureVM()
        {
            this._Converter = new TemperatureConverter();
            this.TemperatureUnits = Settings.TemperatureUnits;
        }

        /// <summary>
        /// Stores the temperature value for the first textbox / dropdown
        /// </summary>
        public float Temp1
        {
            get { return this._Converter.ConvertBack(this._Celsius, ""); }
            set
            {
                float cels = this._Converter.Convert(value, "");
                if (cels != this._Celsius)
                {
                    this._Celsius = cels;
                    RaisePropertyChanged("Temp1");
                }
            }
        }

        /// <summary>
        ///         /// <summary>
        /// Stores the temperature value for the second textbox / dropdown
        /// </summary>
        /// </summary>
        public float Temp2
        {
            get { return this._Converter.ConvertBack(this._Celsius, ""); }
            set
            {
                float cels = this._Converter.Convert(value, "");
                if (cels != this._Celsius)
                {
                    this._Celsius = cels;
                    RaisePropertyChanged("Temp2");
                }
            }
        }


        //public float Temp2
        //{
        //    get { return _Celsius; }
        //    set
        //    {
        //        if (value != this._Celsius)
        //        {
        //            this._Celsius = value;
        //            RaisePropertyChanged("Celsius");
        //        }
        //    }
        //}

  
    }
}
