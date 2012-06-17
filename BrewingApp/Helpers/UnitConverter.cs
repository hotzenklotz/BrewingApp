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

namespace BrewingApp.Helpers
{
    internal static class UnitConverter
    {

        internal static float CelsiusToFahrenheit(float celsius)
        {
            return celsius * 1.8f + 32.0f;
        }

        internal static float FahrenheitToCelsius(float fahrenheit)
        {
            return ((fahrenheit - 32.0f) * 5.0f) / 9.0f;
        }

        internal static float MetricToUSFluid(float liter, string unit)
        {
            float factor = 1.0f;
            switch(unit)
            {
                case "US Gallon":
                    factor = 0.264172052f;
                    break;
                case "US Ounce":
                    factor =  33.8140227f; 
                    break;
                default:
                    factor = 1.0f;
                     break;
            }
            return liter * factor;
        }

        internal static float USToMetricFluid(float value, string unit)
        {
            float factor = 1.0f;
            switch (unit)
            {
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

        internal static float MetricToLiter(float value, string unit)
        {
            float factor;
            switch (unit)
            {
                case "Liter":
                    factor = 1.0f;
                    break;
                case "Milliliter":
                    factor = 0.001f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return value * factor;
        }

        internal static float LiterToMetric(float liter, string unit)
        {
            float factor;
            switch (unit)
            {
                case "Liter":
                    factor = 1.0f;
                    break;
                case "Milliliter":
                    factor = 1000.0f;
                    break;
                default:
                    factor = 1.0f;
                    break;
            }
            return liter * factor;
        }
    }
}
