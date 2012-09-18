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
using System.ComponentModel;
using BrewingApp.Helpers;
using BrewingApp.Models;

namespace BrewingApp.Data
{
   public class UnitConversion 
    {
       private Temperature _Temperature;
       private Volume _Volume;

       public Temperature Temperature
       {
           get { return this._Temperature;}
           set {}
       }

       public Volume Volume
       {
           get { return this._Volume; }
           set { }
       }

       public UnitConversion() 
       {
           this._Temperature = new Temperature();
           this._Volume = new Volume();
       }
       
    }
}
