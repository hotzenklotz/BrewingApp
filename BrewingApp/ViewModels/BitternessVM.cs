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
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.IO.IsolatedStorage;

namespace BrewingApp.ViewModels
{
    public class BitternessVM
    {
        private ICommand _RemoveCommand;
        private ICommand _AddCommand;

        public ObservableCollection<HopListItem> HopList { get; set; }
        public float SpecificGravity { get; set; }
        public float BatchVolume { get; set; }
        public float IBU { get; set; }

        public BitternessVM()
        {
            //add at least one item by default to fill the list
            HopList = new ObservableCollection<HopListItem>();

            HopListItem item = new HopListItem();
            item.SelectionChanged += calculateIBU;
            HopList.Add(item);

            //commands for dropdown menu / applicationBar
            this._RemoveCommand = new RelayCommand<HopListItem>(RemoveAction);
            this._AddCommand = new RelayCommand<HopListItem>(AddAction);

            //default Values
            BatchVolume = 0;
            SpecificGravity = 1.010f;

        }

        public ICommand RemoveCommand
        {
            get { return this._RemoveCommand;  }
        }

        private void RemoveAction(HopListItem item)
        {
            if ( item != null )
                HopList.Remove(item);
        }

        public ICommand AddCommand
        {
            get { return this._AddCommand; }
        }

        private void AddAction(HopListItem item)
        {
            HopList.Add( new HopListItem() );
        }

        /// <summary>
        /// Calculates the IBU (international bitterness units) based on the amount of hops, their boil time and alpha acids
        /// Formulas based on the work of Rager / Tinseth
        /// http://www.rockhoppersbrewclub.com/wiki/calculating-ibus
        /// </summary>
        public void calculateIBU(object sender, TextChangedEventArgs e)
        {
            string formula = (string) IsolatedStorageSettings.ApplicationSettings["IBUFormula"];

            IBU = 0;
       

            switch ( formula )
            {
                case "Rager" :

                    float GravityAdjustment;
                    if (SpecificGravity > 1.050)
                    {
                        GravityAdjustment = (SpecificGravity - 1.050f) / 0.2f;
                    }
                    else {
                        GravityAdjustment = 0.0f;
                    }

                    foreach (HopListItem hop in HopList)
                    {
                        float utilization = 18.11f + 13.86f * (float) Math.Tanh( hop.BoilTime - 31.32f) / 18.27f;
                        IBU += hop.Amount * hop.AlphaAcid * utilization * 1000 / BatchVolume * (1 + GravityAdjustment);
                    }
                    
                    break;

                case "Tinseth" :
                    break;
                default:
                    break;
            }

        }




    }

}
