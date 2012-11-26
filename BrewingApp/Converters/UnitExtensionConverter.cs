using System.Windows.Data;
using System;
using BrewingApp.Models;

namespace BrewingApp.Converters
{
    /// <summary>
    /// Converts all measurement unit' names to their equivalent abbreviations.
    /// </summary>
    public class UnitExtensionConverter : IValueConverter
    {

        public static string Convert( object BindingValue, string parameter)
        {
            string extension = "";
            
            if (parameter == "Volume")
                switch (Settings.VolumeUnit)
                {
                    case "Liter":
                        extension = "l";
                        break;
                    case "Milliliter":
                        extension = "ml";
                        break;
                    case "US Gallon":
                        extension = "gal";
                        break;
                    case "US Ounce":
                        extension = "fl oz";
                        break;
                    case "US Quart":
                        extension ="qt";
                        break;                
                    default:
                        break;
                }

            else if (parameter == "Weight") 
                switch (Settings.WeightUnit)
                {
                    case "Gram":
                        extension ="g";
                        break;
                    case "Kilogram":
                        extension = "kg";
                        break;
                    case "Ounce":
                        extension = "oz";
                        break;
                    case "Pound":
                        extension = "lb";
                        break;
                    default:
                        break;
                }

            else if (parameter == "Temperature")
                switch (Settings.TemperatureUnit)
                {
                    case "Celsius":
                        extension = "°C";
                        break;
                    case "Fahrenheit":
                        extension = "°F";
                        break;
                    default: 
                        break;
                }

            return extension;

        }


        // One way only - setter is not needed
        public static string ConvertBack(object BindingValue, string parameter)
        {
            return "";
        }


        //interface for IValueConverter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert( value, (string) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConvertBack( value, (string)parameter);
        }

    }
}
