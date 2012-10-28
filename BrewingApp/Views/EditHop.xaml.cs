using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Models;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using BrewingApp.Other;

namespace BrewingApp.Views
{
    public partial class EditHop : PhoneApplicationPage, INotifyPropertyChanged
    {      
        
        private string _SelectedItem;
        private Hop _HopItem = new Hop();

        #region public properties
        public Dictionary<string, Hop> _HopVarities;

        public string SelectedItem
        {
            //update the AlphaAcid textbox both on setter / getter to account for
            //inital setting of the selected item and user inputs
            get { 
                AlphaAcid = this._HopVarities[this._SelectedItem].AlphaAcid;
                NotifyPropertyChanged("AlphaAcid");
                return this._SelectedItem;
            }
            set
            {
                this._SelectedItem = value;
                _HopItem.Name = value;
                AlphaAcid = this._HopVarities[value].AlphaAcid;
                NotifyPropertyChanged("AlphaAcid");
            }
        }

        public int BoilTime
        {
            get { return _HopItem.BoilTime; }
            set { _HopItem.BoilTime = value; }
        }

        public float AlphaAcid
        {
            get { return _HopItem.AlphaAcid; }
            set { _HopItem.AlphaAcid = value;; }
        }

        public float Amount
        {
            get { return _HopItem.Amount; }
            set { _HopItem.Amount = value; }
        }

        #endregion  

        public EditHop()
        {
            InitializeComponent();

            //load the hops varities for the dropdown and set them
            this._HopVarities = Hop.loadHopVarities();
            this._SelectedItem = this._HopVarities.Keys.First();
            hopVarityPicker.ItemsSource = this._HopVarities.Keys;

            this.DataContext = this;

            this._HopItem = PhoneApplicationService.Current.State["EditItem"] as Hop;
            this._SelectedItem = this._HopItem.Name;
            NotifyPropertyChanged("SelectedItem");
            NotifyPropertyChanged("AlphaAcid");
            NotifyPropertyChanged("Amount");
            NotifyPropertyChanged("BoilTime");
        }

        /// <summary>
        /// Raise an event to inform the bitterness pivot to calculate the IBU
        /// </summary>
        public void updateValues()
        {
            PhoneApplicationService.Current.State["EditItem"] = this._HopItem;
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage("BitternessVM"));
            NavigationService.GoBack();
        }


        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            updateValues();
        }

        //INotifyPropertyChanged Implementation
        #region
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

        #endregion

        private void btnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updateValues();
        }
    }
}