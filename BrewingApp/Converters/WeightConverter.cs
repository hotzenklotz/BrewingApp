using System;
using System.Windows.Data;
using System.IO.IsolatedStorage;

namespace BrewingApp.Converters
{
    public class WeightConverter : IValueConverter
    {
        private string _DefaultUnit;

        /// <summary>
        /// Converts any weight to "grams", the only internally used measurement unit for weghts
        /// </summary>
        public float Convert(float value, string parameter)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }


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

        public float ConvertBack(float value, string parameter)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter != null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }

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


        public WeightConverter()
        {
            this._DefaultUnit = (string)IsolatedStorageSettings.ApplicationSettings["WeightUnit"];
        }

        // provide compatible interface for IValueConverter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert((float)value, (string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return ConvertBack((float)value, (string)parameter);
        }
    }

}
