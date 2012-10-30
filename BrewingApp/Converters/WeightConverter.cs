using System;
using System.Windows.Data;
using BrewingApp.Models;

namespace BrewingApp.Converters
{
    public class WeightConverter : IValueConverter
    {
        /// <summary>
        /// Converts any weight to "grams", the only internally used measurement unit for weights
        /// </summary>
        public static float Convert(float value, string parameter = null)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = Settings.WeightUnit; } //default unit


            float factor = 1.0f;
            switch (measurementUnit)
            {
                case "Gram":
                    factor = 1.0f;
                    break;
                case "Kilogram":
                    factor = 0.001f;
                    break;
                case "Ounce":
                    factor = 0.035274f;
                    break;
                case "Pound":
                    factor = 0.00220462f;
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
            { measurementUnit = Settings.WeightUnit; }

            float factor = 1.0f;
            switch (measurementUnit)
            {
                case "Gram":
                    factor = 1.0f;
                    break;
                case "Kilogram":
                    factor = 1000.0f;
                    break;
                case "Ounce":
                    factor = 28.3495f;
                    break;
                case "Pound":
                    factor = 453.592f;
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

            return ConvertBack(floatValue, (string)parameter);
        }
    }

}
