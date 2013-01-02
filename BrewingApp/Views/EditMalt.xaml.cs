using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using BrewBuddy.Models;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using BrewBuddy.Other;

namespace BrewBuddy.Views
{
    public partial class EditMalt : PhoneApplicationPage, INotifyPropertyChanged
    {      
        
        private string _SelectedItem;
        private Malt _MaltItem = new Malt();

        #region public properties
        public Dictionary<string, Malt> _MaltVarities;

        public string SelectedItem
        {
            //update the PPG textbox both on setter / getter to account for
            //inital setting of the selected item and user inputs
            get { 
                PPG = this._MaltVarities[this._SelectedItem].PPG;
                NotifyPropertyChanged("PPG");
                return this._SelectedItem;
            }
            set
            {
                this._SelectedItem = value;
                _MaltItem.Name = value;
                PPG = this._MaltVarities[value].PPG;
                NotifyPropertyChanged("PPG");
            }
        }

        public float PPG
        {
            get { return _MaltItem.PPG; }
            set { _MaltItem.PPG = value;; }
        }

        public float Amount
        {
            get { return _MaltItem.Amount; }
            set { _MaltItem.Amount = value; }
        }

        #endregion  

        public EditMalt()
        {
            InitializeComponent();

            //load the hops varities for the dropdown and set them
            this._MaltVarities = Malt.loadMaltVarities();
            this._SelectedItem = this._MaltVarities.Keys.First();
            maltVarityPicker.ItemsSource = this._MaltVarities.Keys;

            this.DataContext = this;

            this._MaltItem = PhoneApplicationService.Current.State["EditItem"] as Malt;

            float PPG = this._MaltItem.PPG; //othwise will be overwritten by seting slectedItem 

            this._SelectedItem = this._MaltItem.Name;
            NotifyPropertyChanged("SelectedItem");

            this._MaltItem.PPG = PPG;
            NotifyPropertyChanged("Amount");
            NotifyPropertyChanged("PPG");
        }

        /// <summary>
        /// Raise an event to inform the bitterness pivot to calculate the IBU
        /// </summary>
        public void updateValues()
        {
            PhoneApplicationService.Current.State["EditItem"] = this._MaltItem;
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage("GravityVM"));
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