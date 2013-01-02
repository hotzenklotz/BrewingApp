using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using BrewBuddy.Converters;

namespace BrewBuddy.Models
{
    public class Hop
    {

        public int BoilTime { get; set; }
        public float AlphaAcid { get; set; }
        public string Name { get; set; }
        public float Amount{ get; set; }

        public static Dictionary<string, Hop> _VarityCache{ get; set; }

        static public Dictionary<string, Hop> loadHopVarities()
        {
            if (_VarityCache != null)
                return _VarityCache;

            XElement rootElement = XElement.Load("XML/HopVarities.xml");

            Dictionary<string, Hop> dict = new Dictionary<string, Hop>();
            foreach (var el in rootElement.Elements())
            {
                //TODO profile Memory cost
                Hop tmp = new Hop();

                tmp.Name = (string)el.Attribute("name");
                tmp.AlphaAcid = (float)el.Attribute("alpha");

                dict.Add(tmp.Name, tmp);
            }
            _VarityCache = dict;

            return _VarityCache;
        }
    }
}
