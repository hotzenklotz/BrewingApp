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

namespace BrewingApp
{
    public partial class HopListItem : UserControl
    {
        public event EventHandler SelectionChanged;
        public int BoilTime { get; set; }
        public float AlphaAcid { get; set; }
        public float Amount { get; set; }

        private Dictionary<string, float> hopVarities;
 
        public HopListItem()
        {
            InitializeComponent();

            this.hopVarities = loadHopXML();
            this.DataContext = this;

            hopVarityPicker.ItemsSource = this.hopVarities.Keys;

        }

        private Dictionary<string, float> loadHopXML()
        {

            XElement rootElement = XElement.Load("XML/HopVarities.xml");

            Dictionary<string, float> dict = new Dictionary<string, float>();
            foreach (var el in rootElement.Elements())
            {
                dict.Add( (string) el.Attribute("name"), (float) el.Attribute("alpha"));
            }

            return dict;
        }

        /// <summary>
        /// Raise an event to inform the pivot containing the list to calculate the IBU
        /// </summary>
        private void updateSelection()
        {
            if (this.SelectionChanged != null)
                this.SelectionChanged(new object(), new EventArgs());
        }

    }
}
