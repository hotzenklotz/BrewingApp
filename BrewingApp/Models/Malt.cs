
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

        private static Dictionary<string, Malt> _VarityCache{ get; set; }

        public Malt()
        {
            this._Converter = new WeightConverter();
        }

        /// <summary>
        /// Parse an XML file full of Malt varities and their properties
        /// </summary>
        /// <returns></returns>
        static public Dictionary<string, Malt> loadMaltVarities()
        {
            if (_VarityCache != null)
                return _VarityCache;

            XElement rootElement = XElement.Load("XML/MaltVarities.xml");

            Dictionary<string, Malt> dict = new Dictionary<string, Malt>();
            foreach (var el in rootElement.Elements())
            {
                //TODO profile Memory cost !?!?
                Malt tmp = new Malt();

                tmp.Name = (string)el.Attribute("name");
                tmp.Lovibond = (float)el.Attribute("lovibond");
                tmp.PPG = (float)el.Attribute("ppg");
                tmp.isMashable = (bool)el.Attribute("mashable");

                dict.Add(tmp.Name, tmp);
            }
            _VarityCache = dict;

            return _VarityCache;
        }


    }
}
