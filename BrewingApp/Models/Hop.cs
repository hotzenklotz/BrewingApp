using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BrewingApp.Models
{
    public class Hop
    {
        public int BoilTime { get; set; }
        public float AlphaAcid { get; set; }
        public float Amount { get; set; }
        public string Name { get; set; }

        public Hop()
        {
 
        }

        public Hop Clone()
        {
            return this.Clone();
        }

    }
}
