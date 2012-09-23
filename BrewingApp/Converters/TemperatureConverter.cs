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
using System.Windows.Data;
using System.IO.IsolatedStorage;

namespace BrewingApp.Converters
{
    /// <summary>
    /// Converts any Temperature to "Celsius", the only internally used measurement unit for volumes
    /// </summary>
    public class TemperatureConverter : IValueConverter
    {
        private string _DefaultUnit;

        public float Convert(float value, string parameter)
        {
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }

            float temp = 1.0f;
            switch (measurementUnit)
                {
                case "Celsius" :
                    temp = value;
                    break;
                case "Fahrenheit" :
                    temp = ((value - 32.0f) * 5.0f) / 9.0f;
                    break;
                default:
                    break;
            }

            return temp;
 
        }

        public float ConvertBack(float value, string parameter)
        {
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }

            float temp = 1.0f;
            switch (measurementUnit)
            {
                case "Celsius" :
                    temp = value;
                    break;
                case "Fahrenheit" :
                    temp = value * 1.8f + 32.0f; 
                    break;
                default:
                    break;
            }

            return temp;
        }

        public TemperatureConverter()
        {
            this._DefaultUnit = (string)IsolatedStorageSettings.ApplicationSettings["TemperatureUnit"]; 
        }

        //interface for IValueConverter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert( (float) value, (string) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConvertBack((float)value, (string)parameter);
        }
    }
}
