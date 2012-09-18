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
using System.ComponentModel;
using System.Collections.ObjectModel;
using BrewingApp.Helpers;

namespace BrewingApp.Models
{
    public class Volume : INotifyPropertyChanged
    {

        private ObservableCollection<string> _USUnitList = new ObservableCollection<string>() 
        {
            "US Gallon",
            "US Ounce"

        };

        private ObservableCollection<string> _MetricUnitList = new ObservableCollection<string>() 
        {
            "Liter",
            "Milliliter"

        };




        private float _liter;
        private string _USUnit;
        private string _MetricUnit;

        public string USUnit
        {
            get
            {
                return this._USUnit;
            }

            set
            {
                _USUnit = value;
                FluidPropertiesChanged();
            }
        }

        public string MetricUnit
        {
            get { return this._MetricUnit; }
            set
            {
                this._MetricUnit = value;
                FluidPropertiesChanged();
            }
        }

        public float USFluid
        {
            get { return UnitConverter.MetricToUSFluid(this._liter, this.USUnit); }
            set
            {
                float tmpLiter = UnitConverter.USToMetricFluid(value, this.USUnit);
                if (MetricFluid != tmpLiter)
                    this._liter = tmpLiter;
                FluidPropertiesChanged();
            }
        }

        public float MetricFluid
        {
            get { return UnitConverter.LiterToMetric(this._liter, this.MetricUnit); }
            set
            {
                this._liter = UnitConverter.MetricToLiter(value, this.MetricUnit);
                FluidPropertiesChanged();
            }
        }

        private void FluidPropertiesChanged()
        {
            NotifyPropertyChanged("USFluid");
            NotifyPropertyChanged("MetricFluid");
        }


        // Declare the PropertyChanged event.
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event, 
        // passing the source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
