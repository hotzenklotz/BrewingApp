﻿using System;
using System.Windows.Data;
using System.IO.IsolatedStorage;

namespace BrewingApp.Converters
{
    public class VolumeConverter : IValueConverter
    {
        private string _DefaultUnit;

        /// <summary>
        /// Converts any volume to "liters", the only internally used measurement unit for volumes
        /// </summary>
        public float Convert(float value, string parameter)
        {
            //if no measurement unit is provided use the default unit form the setting
            string measurementUnit;

            if (parameter == null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }


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

            if (parameter == null)
            { measurementUnit = (string)parameter; }
            else
            { measurementUnit = this._DefaultUnit; }

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
                default:
                    factor = 1.0f;
                    break;
            }
            return value * factor;
        }


        public VolumeConverter()
        {
            this._DefaultUnit = (string)IsolatedStorageSettings.ApplicationSettings["VolumeUnit"];
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