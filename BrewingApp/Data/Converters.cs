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
using System.ComponentModel;
using BrewingApp.Helpers;

namespace BrewingApp.Data
{
   public class Converters : INotifyPropertyChanged
    {
        #region Temperature Properties

        private float _celsius;

        public float Fahrenheit
        {
            get { return UnitConverter.CelsiusToFahrenheit(_celsius); }
            set
            {
                float cels = UnitConverter.FahrenheitToCelsius(value);
                if (cels != _celsius)
                {
                    _celsius = cels;
                    TempPropertiesChanged();
                }
            }
        }

        public float Celsius
        {
            get { return _celsius; }
            set
            {
                if (value != _celsius)
                {
                    _celsius = value;
                    TempPropertiesChanged();
                }
            }
        }

        private void TempPropertiesChanged()
        {
            NotifyPropertyChanged("Fahrenheit");
            NotifyPropertyChanged("Celsius");
        }

        #endregion

        #region Fluid Properties
        private float _liter;
        private string _USUnit;
        private string _MetricUnit;

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
            NotifyPropertyChanged("USFluid");
            NotifyPropertyChanged("MetricFluid");
        }

        #endregion
        // Declare the PropertyChanged event.
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event, 
        // passing the source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
