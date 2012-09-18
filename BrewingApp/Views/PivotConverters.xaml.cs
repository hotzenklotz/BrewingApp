using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using BrewingApp.Data;

namespace BrewingApp.Pages
{
    public partial class PivotConverters : PhoneApplicationPage
    {
        private UnitConversion DataModel;

        public PivotConverters()
        {
            InitializeComponent();

            this.DataModel = new UnitConversion();
            DataContext = this.DataModel;

            //USFluidPicker.SelectionChanged += new SelectionChangedEventHandler(USFluidPicker_SelectionChanged);
            //MetricFluidPicker.SelectionChanged +=new SelectionChangedEventHandler(MetricFluidPicker_SelectionChanged);

        }

        private void USFluidPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPicker picker = (ListPicker)sender;
            this.DataModel.Volume.USUnit = picker.SelectedItem.ToString();
        }

        private void MetricFluidPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPicker picker = (ListPicker)sender;
            this.DataModel.Volume.MetricUnit = picker.SelectedItem.ToString();
        }


    }
}   