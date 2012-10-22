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
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Other;
using BrewingApp.Models;
using System.Xml.Linq;
using System.ComponentModel;

namespace BrewingApp.Views
{
    public partial class EditHop : PhoneApplicationPage, INotifyPropertyChanged
    {
        public EditHop()
        {
            InitializeComponent();

            Messenger.Default.Register<EditHopMessage>
            (
                this,
                (message) => { this._HopItem = message.HopItem; }
            );


            //load the hops varities for the dropdown and set them
            this._HopVarities = loadHopXML();
            this._SelectedItem = this._HopVarities.Keys.First();
            hopVarityPicker.ItemsSource = this._HopVarities.Keys;

            this.DataContext = this;

        }

        private string _SelectedItem;
        private Hop _HopItem { get; set; }

        #region public properties
        public Dictionary<string, float> _HopVarities;

        public string SelectedItem
        {
            //update the AlphaAcid textbox both on setter / getter to account for
            //inital setting of the selected item and user inputs
            get { 
                AlphaAcid = this._HopVarities[this._SelectedItem];
                NotifyPropertyChanged("AlphaAcid");
                return this._SelectedItem;
            }
            set
            {
                this._SelectedItem = value;
                _HopItem.Name = value;
                AlphaAcid = this._HopVarities[value];
                NotifyPropertyChanged("AlphaAcid");
            }
        }

        public int BoilTime
        {
            get { return _HopItem.BoilTime; }
            set { _HopItem.BoilTime = value; updateValues(); }
        }

        public float AlphaAcid
        {
            get { return _HopItem.AlphaAcid; }
            set { _HopItem.AlphaAcid = value; updateValues(); }
        }

        public float Amount
        {
            get { return _HopItem.Amount; }
            set { _HopItem.Amount = value; updateValues(); }
        }

        #endregion



        private Dictionary<string, float> loadHopXML()
        {

            XElement rootElement = XElement.Load("XML/HopVarities.xml");

            Dictionary<string, float> dict = new Dictionary<string, float>();
            foreach (var el in rootElement.Elements())
            {
                dict.Add((string)el.Attribute("name"), (float)el.Attribute("alpha"));
            }

            return dict;
        }

        
        /// <summary>
        /// Raise an event to inform the pivot containing the list to calculate the IBU
        /// </summary>
        private void updateValues()
        {
            //Messenger.Default.Send<string>("HopListValuesChanged");
        }

        //INotifyPropertyChanged Implementation

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