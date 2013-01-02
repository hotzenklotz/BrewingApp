using GalaSoft.MvvmLight;
using BrewBuddy.ViewModels;

namespace BrewBuddy.ViewModels
{
   public class UnitConversionVM
    {
       private ViewModelBase _Temperature;
       private ViewModelBase _Volume;
       private ViewModelBase _Weight;

       public ViewModelBase Weight
       {
           get { return this._Weight; }
           set { }
       }

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
           this._Weight = new WeightVM();
       }
       
    }
}
