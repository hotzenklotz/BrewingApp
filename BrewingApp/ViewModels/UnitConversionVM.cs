using GalaSoft.MvvmLight;
using BrewingApp.ViewModels;

namespace BrewingApp.ViewModels
{
   public class UnitConversionVM
    {
       private ViewModelBase _Temperature;
       private ViewModelBase _Volume;

       public ViewModelBase Temperature
       {
           get { return this._Temperature;}
           set {}
       }

       public ViewModelBase Volume
       {
           get { return this._Volume; }
           set { }
       }

       public UnitConversionVM() 
       {
           this._Temperature = new TemperatureVM();
           this._Volume = new VolumeVM();
       }
       
    }
}
