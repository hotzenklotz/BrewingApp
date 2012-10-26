
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using BrewingApp.Converters;

namespace BrewingApp.Models
{
    public class Malt
    {
        private WeightConverter _Converter;

        public string Name { get; set; }
        public float PPG { get; set; }
        public float Lovibond { get; set; } // present in the XML not loaded, though
        public float Amount{ get; set; }
        public bool isMashable { get; set; }

        public static Dictionary<string, float> VarityCache{ get; set; }

        public Malt()
        {
            this._Converter = new WeightConverter();

            string firstHop = Malt.loadMaltVarities().Keys.First();

            this.Name = firstHop;
            this.PPG = Malt.loadMaltVarities()[firstHop];
            this.Amount = 10;
        }

        static public Dictionary<string, float> loadMaltVarities()
        {
            if (VarityCache != null)
                return VarityCache;

            XElement rootElement = XElement.Load("XML/MaltVarities.xml");

            Dictionary<string, float> dict = new Dictionary<string, float>();
            foreach (var el in rootElement.Elements())
            {
                dict.Add((string)el.Attribute("name"), (float)el.Attribute("ppg"));
            }
            VarityCache = dict;

            return VarityCache;
        }

        /// <summary>
        /// Output a formated string, so one directly bind lists of Malt nicely
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}\n{1,3:f2} {2}", this.Name, this._Converter.Convert(this.Amount), Settings.WeightUnit);
        }
    }
}
