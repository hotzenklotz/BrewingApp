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
using BrewingApp.Models;
using System.IO.IsolatedStorage;

namespace BrewingApp.Converters
{
    public class TemperatureConverter : IValueConverter
    {
        private string _DefaultUnit;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            float factor = 1.0f;
            switch ( this._DefaultUnit )
            {
                case "Liter":
                    factor = 1.0f;
                    break;
                case "Milliliter":
                    factor = 0.001f;
                    break;
                case "US Gallon":
                    factor = 0.264172052f;
                    break;
                case "US Ounce":
                    factor = 33.8140227f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return (float) value * factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            float factor = 1.0f;
            switch ( this._DefaultUnit )
            {
                case "Liter":
                    factor = 1.0f;
                    break;
                case "Milliliter":
                    factor = 1000.0f;
                    break;
                case "US Gallon":
                    factor = 3.78541178f;
                    break;
                case "US Ounce":
                    factor = 0.0295735296f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return (float) value * factor;
        }

        public TemperatureConverter()
        {
            this._DefaultUnit = (string) IsolatedStorageSettings.ApplicationSettings["TemperatureUnit"];
        }

    }
}
