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
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using BrewingApp.Models;

namespace BrewingApp
{
    public partial class HopListPopup : UserControl, INotifyPropertyChanged
    {
        private string _SelectedItem;
        public Hop HopItem { get; set; }

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
                HopItem.Name = value;
                AlphaAcid = this._HopVarities[value];
                NotifyPropertyChanged("AlphaAcid");
            }
        }

        public int BoilTime
        {
            get { return HopItem.BoilTime; }
            set { HopItem.BoilTime = value; updateValues(); }
        }

        public float AlphaAcid
        {
            get { return HopItem.AlphaAcid; }
            set { HopItem.AlphaAcid = value; updateValues(); }
        }

        public float Amount
        {
            get { return HopItem.Amount; }
            set { HopItem.Amount = value; updateValues(); }
        }

        #endregion

        public HopListPopup()
        {
            InitializeComponent();

            //load the hops varities for the dropdown and set them
            this._HopVarities = loadHopXML();
            this._SelectedItem = this._HopVarities.Keys.First();
            hopVarityPicker.ItemsSource = this._HopVarities.Keys;

            this.DataContext = this;

        }

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
            Messenger.Default.Send<string>("HopListValuesChanged");
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
