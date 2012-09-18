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
using System.Collections.ObjectModel;
using BrewingApp.Helpers;

namespace BrewingApp.Models
{
    public class Temperature : INotifyPropertyChanged
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
