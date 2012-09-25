using System;
using System.Windows.Data;
using BrewingApp.Models;

namespace BrewingApp.Converters
{
    /// <summary>
    /// Converts any Temperature to "Celsius", the only internally used measurement unit for volumes
    /// </summary>
    public class TemperatureConverter : IValueConverter
    {

        public float Convert(float value, string parameter)
        {
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string) parameter; }
            else
            { measurementUnit = Settings.TemperatureUnit; } //default settings

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
            { measurementUnit = Settings.TemperatureUnit; }

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
