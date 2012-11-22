using System;
using System.Windows.Data;
using System.IO.IsolatedStorage;
using BrewingApp.Models;

namespace BrewingApp.Converters
{
    public class VolumeConverter : IValueConverter
    {

        /// <summary>
        /// Converts any volume to "liters", the only internally used measurement unit for volumes
        /// </summary>
        public static float Convert(float value, string parameter = null)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = Settings.VolumeUnit; }


            float factor = 1.0f;
            switch (measurementUnit)
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
                case "US Quart":
                    factor = 1.056688209432594f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return value * factor;
        }

        public static float ConvertBack(float value, string parameter = null)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = Settings.VolumeUnit; }

            float factor = 1.0f;
            switch (measurementUnit)
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
                case "US Quart":
                    factor = 0.946352946f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return value * factor;
        }

        // provide compatible interface for IValueConverter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert((float)value, (string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            float floatValue;
            float.TryParse((string)value, out floatValue);

            return ConvertBack( floatValue, (string)parameter);
        }
    }

}
