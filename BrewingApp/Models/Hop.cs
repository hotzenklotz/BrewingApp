using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace BrewingApp.Models
{
    public class Hop
    {
        public int BoilTime { get; set; }
        public float AlphaAcid { get; set; }
        public float Amount { get; set; }
        public string Name { get; set; }

        public static Dictionary<string, float> VarityCache{ get; set; }

        public Hop()
        {
            string firstHop = Hop.loadHopVarities().Keys.First();

            this.Name = firstHop;
            this.AlphaAcid = Hop.loadHopVarities()[firstHop];
            this.BoilTime = 60;
            this.Amount = 10;

        }

        public Hop Clone()
        {
            return this.Clone();
        }

        static public Dictionary<string, float> loadHopVarities()
        {
            if (VarityCache != null)
                return VarityCache;

            XElement rootElement = XElement.Load("XML/HopVarities.xml");

            Dictionary<string, float> dict = new Dictionary<string, float>();
            foreach (var el in rootElement.Elements())
            {
                dict.Add((string)el.Attribute("name"), (float)el.Attribute("alpha"));
            }
            VarityCache = dict;

            return VarityCache;
        }

        /// <summary>
        /// Output a formated string, so one directly bind lists of Hops nicely
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1:f2}%) \n{2,3:f2} {3} @ {4}min ", this.Name, this.AlphaAcid, this.Amount, Settings.WeightUnit, this.BoilTime);
        }
    }
}
