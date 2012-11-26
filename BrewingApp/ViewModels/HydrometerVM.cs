using GalaSoft.MvvmLight;
using System.Windows;
using BrewingApp.Converters;

namespace BrewingApp.ViewModels
{
    public class HydrometerVM : ViewModelBase
    {

        private int _Temperature;
        private int _Calibration;
        private float _Gravity;

        private string _ErrorMessage;

        private float[] _Delta = new float[] { -0.0009f, -0.0009f, -0.0009f, -0.0009f, -0.0009f, -0.0009f, -0.0008f, -0.0008f, -0.0007f, -0.0007f, -0.0006f, -0.0005f, -0.0004f, -0.0003f, -0.0001f, 0f, 0.0002f, 0.0003f, 0.0005f, 0.0007f, 0.0009f, 0.0011f, 0.0013f, 0.0016f, 0.0018f, 0.0021f, 0.0023f, 0.0026f, 0.0029f, 0.0032f, 0.0035f, 0.0038f, 0.0041f, 0.0044f, 0.0047f, 0.0051f, 0.0054f, 0.0058f, 0.0061f, 0.0065f, 0.0069f, 0.0073f, 0.0077f, 0.0081f, 0.0085f, 0.0089f, 0.0093f, 0.0097f, 0.0102f, 0.0106f, 0.0110f, 0.0114f, 0.0118f, 0.0122f, 0.0126f, 0.0130f, 0.0135f, 0.0140f, 0.0145f, 0.0150f, 0.0155f, 0.0160f, 0.0165f, 0.0171f, 0.0177f, 0.0183f, 0.0189f, 0.0195f, 0.0201f, 0.0207f, 0.0213f, 0.0219f, 0.0225f, 0.0231f, 0.0237f, 0.0243f, 0.0249f, 0.0255f, 0.0261f, 0.0273f, 0.0267f };

        public HydrometerVM()
        {

            this._ErrorMessage = string.Format("In order to provide useful results, we can only correct temperatures below {0}° and calibration temperatures between {1}° and {2}°",
                               TemperatureConverter.ConvertBack(80), TemperatureConverter.ConvertBack(10), TemperatureConverter.ConvertBack(24));

            //default values
            this.Temperature = 50;
            this.Calibration = 20;
            this.Gravity = 1.040f;

     }

        public int Temperature
        {
            get { return this._Temperature; }
            set 
            {
                this._Temperature = value;
                if (value < 0 || value > 80)
                    MessageBox.Show(this._ErrorMessage,"Sorry", MessageBoxButton.OK);

                calculateCorrection();
                RaisePropertyChanged("Temperature");
            }
        }

        public int Calibration
        {
            get { return this._Calibration; }
            set
            {
                this._Calibration = value;

                if ( value < 10 || value > 24 )
                    MessageBox.Show(this._ErrorMessage, "Sorry", MessageBoxButton.OK);

                calculateCorrection();
                RaisePropertyChanged("Calibration");
            }
        }

        public float Gravity
        {
            get { return this._Gravity; }
            set
            {
                this._Gravity = value;
                calculateCorrection();
                RaisePropertyChanged("Reading");
            }
        }

        public float Correction
        {
            get;
            set;
        }

        /// <summary>
        /// Calculate the correction of a specific gravity reading at certain temperature.
        /// Unfortuneatly there no formula that really is correct for all temperature values.
        /// http://www.brewersfriend.com/hydrometer-temp/
        /// 
        /// For 10°C - 40°C : 
        /// http://hbd.org/brewery/library/HydromCorr0992.html
        /// 
        /// Alternative
        /// http://fabier.de/biercalcs.html
        /// </summary>
        private void calculateCorrection()
        {
            int calibrationOffset = 15 - this._Calibration;
            float difference = 0;

            if (this._Temperature < 0 || this._Temperature > 80) {
                Correction = 0;
                return;
            }

            if (this._Calibration < 10 || this._Calibration > 24)
            {
                Correction = 0;
                return;
            }


            if ((this._Temperature + calibrationOffset) < 0)
            {
                calibrationOffset = 0;
            }

            difference = this._Delta[this._Temperature + calibrationOffset];

            Correction = this._Gravity + difference;
            RaisePropertyChanged("Correction");

        }


    }
}
