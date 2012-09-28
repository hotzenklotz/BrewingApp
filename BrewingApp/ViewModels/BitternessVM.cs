using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Models;
using System.Windows.Controls.Primitives;

namespace BrewingApp.ViewModels
{
    public class BitternessVM : ViewModelBase 
    {
        private ICommand _RemoveCommand;
        private ICommand _AddCommand;
        private Popup _Popup;

        private int _IBU;
        private float _BatchVolume;
        private float _SpecificGravity;

        #region public properties
        public ObservableCollection<Hop> HopList { get; set; }

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

        public int IBU 
        {
            get { return this._IBU; }
            set { this._IBU = value; RaisePropertyChanged("IBU"); }
        }

        #endregion

        public BitternessVM()
        {
            //add at least one item by default to fill the list
            HopList = new ObservableCollection<Hop>();
            HopList.Add(new Hop());

            //use MVVM LightToolkit messaging for custom controls update notifications
            Messenger.Default.Register<string>
            (
                this, 
                (message) => 
                    { if (message == "HopListValuesChanged") calculateIBU(); }
            );

            //MVVM LightToolkit commands for dropdown menu / applicationBar
            this._RemoveCommand = new RelayCommand<Hop>(RemoveAction);
            this._AddCommand = new RelayCommand<Hop>(AddAction);

            //default Values
            BatchVolume = 20;
            SpecificGravity = 1.010f;

            initPopup();

        }

        private void initPopup()
        {
            this._Popup = new Popup();
            this._Popup.Height = 300;
            this._Popup.Width = 400;
            this._Popup.VerticalOffset = 100;
            HopListPopup control = new HopListPopup();
            this._Popup.Child = control;
            this._Popup.IsOpen = false;
        }

        public ICommand RemoveCommand
        {
            get { return this._RemoveCommand;  }
        }

        private void RemoveAction(Hop item)
        {
            if ( item != null )
                HopList.Remove(item);
        }

        public ICommand AddCommand
        {
            get { return this._AddCommand; }
        }

        private void AddAction(Hop item)
        {
            Hop hop = new Hop();
            displayPopUp(hop);

            HopList.Add( hop );
        }

        private void ModifyAction(Hop item)
        {
            displayPopUp(item);
        }

        private void displayPopUp(Hop item)
        {
            var PopupChild = this._Popup.Child as HopListPopup;
            PopupChild.HopItem = item;

            this._Popup.IsOpen = true;
        }

        /// <summary>
        /// Calculates the IBU (international bitterness units) based on the amount of hops, their boil time and alpha acids
        /// Formulas based on the work of Rager / Tinseth
        /// http://www.rockhoppersbrewclub.com/wiki/calculating-ibus
        /// </summary>
        public void calculateIBU()
        {
            string formula = Settings.HopFormula;

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

                    foreach (Hop hop in HopList)
                    {
                        float utilization = 0.1811f + 0.1386f * (float) Math.Tanh( hop.BoilTime - 31.32f) / 18.27f;
                        IBU += (int) ( hop.Amount * hop.AlphaAcid * utilization * 10 / BatchVolume * (1 + GravityAdjustment) );
                    }
                    
                    break;

                case "Tinseth" :

                    throw new NotImplementedException();
                    break;

                default:
                    break;
            }

        }




    }

}
