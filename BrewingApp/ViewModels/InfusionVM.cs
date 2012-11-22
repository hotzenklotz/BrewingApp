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
using GalaSoft.MvvmLight;
using BrewingApp.Converters;

namespace BrewingApp.ViewModels
{
    public class InfusionVM : ViewModelBase
    {
        private int _WaterTemp;
        private int _TargetTemp;
        private float _WaterAmount;
        private float _GrainAmount;

        public InfusionVM()
        {
        }

        #region public properties

        public int WaterTemp 
        {
            get { return this._WaterTemp; }
            set
            { 
                this._WaterTemp = value;
                RaisePropertyChanged("WaterTemp");
                CalculateInfusion();
            }
        }

        public int TargetTemp
        {
            get { return this._TargetTemp; }
            set
            {
                this._TargetTemp = value;
                RaisePropertyChanged("TargetTemp");
                CalculateInfusion();
            }
        }

        public float WaterAmount
        {
            get { return this._WaterAmount; }
            set
            {
                this._WaterAmount = value;
                RaisePropertyChanged("WaterAmount");
                CalculateInfusion();
            }
        }

        public float GrainAmount
        {
            get { return this._GrainAmount; }
            set
            {
                this._GrainAmount = value;
                RaisePropertyChanged("GrainAmount");
                CalculateInfusion();
            }
        }

        public float InfusionAmount
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Calcluate the amount of water that needs to be added to raise the mash to a target temperature.
        /// http://www.mashspargeboil.com/calculating-strike-water-temperature/
        /// </summary>
        /// <returns></returns>
        private void CalculateInfusion()
        {
            float grainAmountKG = GrainAmount / 1000;
            float boilingWater = TemperatureConverter.ConvertBack(100.0f, null);

            InfusionAmount =(float)((TargetTemp - WaterTemp) * (0.42 * grainAmountKG + WaterAmount) / (boilingWater - TargetTemp));
            RaisePropertyChanged("InfusionAmount");

        }

    }
}
