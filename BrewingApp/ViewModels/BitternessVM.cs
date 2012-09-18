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
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

namespace BrewingApp.ViewModels
{
    public class BitternessVM : ViewModelBase 
    {
        private ICommand _RemoveCommand;
        private ICommand _AddCommand;

        private float _IBU;
        private float _BatchVolume;
        private float _SpecificGravity;

        #region public properties
        public ObservableCollection<HopListItem> HopList { get; set; }

        public float SpecificGravity 
        {
            get { return this._SpecificGravity; }
            set { this._SpecificGravity = value; calculateIBU(); }
        }

        public float BatchVolume 
        {
            get { return this._BatchVolume; }
            set { this._BatchVolume = value; calculateIBU(); }
        }

        public float IBU 
        {
            get { return this._IBU; }
            set { this._IBU = value; RaisePropertyChanged("IBU"); }
        }

        #endregion

        public BitternessVM()
        {
            //add at least one item by default to fill the list
            HopList = new ObservableCollection<HopListItem>();
            HopList.Add(new HopListItem());

            //use MVVM LightToolkit messaging for custom controls update notifications
            Messenger.Default.Register<string>
            (
                this, 
                (message) => 
                    { if (message == "HopListValuesChanged") calculateIBU(); }
            );

            //MVVM LightToolkit commands for dropdown menu / applicationBar
            this._RemoveCommand = new RelayCommand<HopListItem>(RemoveAction);
            this._AddCommand = new RelayCommand<HopListItem>(AddAction);

            //default Values
            BatchVolume = 20;
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
        public void calculateIBU()
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
            System.Diagnostics.Debug.WriteLine(IBU);
        }




    }

}
