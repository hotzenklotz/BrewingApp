using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Models;
using BrewingApp.Other;
using System;
using System.Linq;
using BrewingApp.Converters;

namespace BrewingApp.ViewModels
{
    public class GravityVM : MaltHopBase<Malt> 
    {
       
        private float _Gravity;
        private float _BatchVolume;
        private float _Efficiency;

        #region public properties

        public float BatchVolume 
        {
            get { return this._BatchVolume; }
            set { this._BatchVolume = value; calculateGravity(); }
        }

        public float Efficiency
        {
            get { return this._Efficiency; }
            set { this._Efficiency = value; calculateGravity(); }
        }


        public float Gravity 
        {
            get { return this._Gravity; }
            set { this._Gravity = value; RaisePropertyChanged("Gravity"); }
        }

        #endregion

        public GravityVM() : base()
        {

            //add at least one item by default to fill the list
            ItemList = new ObservableCollection<Malt>();

            Malt firstItem = new Malt();
            setDefaultValues(firstItem);

            ItemList.Add(firstItem);

            //default Values
            BatchVolume = VolumeConverter.ConvertBack(20);
            Efficiency = 70;

            //use MVVM LightToolkit messaging for custom controls update notifications
            Messenger.Default.Register<UpdateViewMessage>
                       (
                           this,
                           (message) =>
                           {
                               if (message.ViewName == "GravityVM")
                                   updateView();
                                   calculateGravity();
                           }
                       );
        }

        public override void editItem(Malt item) 
        {
            base.editItem(item);
            Messenger.Default.Send<NavigateMessage>(new NavigateMessage("/Views/EditMalt.xaml"));
        }

        public override void setDefaultValues(Malt item)
        {
            item.Name = Malt.loadMaltVarities().Values.First().Name;
            item.PPG = Malt.loadMaltVarities().Values.First().PPG;
            item.isMashable = Malt.loadMaltVarities().Values.First().isMashable;
            item.Lovibond = Malt.loadMaltVarities().Values.First().Lovibond;
            item.Amount = 1000;
        }

        /// <summary>
        /// Calculates the original gravity of a sud based on the provided malts and adjuncts
        /// Formula : http://www.brewersfriend.com
        /// </summary>
        /// <returns></returns>
        private void calculateGravity() 
        {

            float gravity = 0.0f;
            float maltGravity = 0.0f;
            float adjunctGravity = 0.0f;//not affeted by boilding efficiency (usually added later)

            foreach (Malt item in ItemList)
            {
                if (item.isMashable)
                    maltGravity += item.PPG * WeightConverter.Convert(item.Amount, "Pound");
                else
                    adjunctGravity += item.PPG * WeightConverter.Convert(item.Amount, "Pound");
            }

            maltGravity *= (this._Efficiency / 100);
            gravity = maltGravity + adjunctGravity; 
            gravity /= VolumeConverter.Convert(this._BatchVolume, "US Gallon");

            //output range 1.000 - 1.1XX
            Gravity = (float) (gravity * 0.001 + 1);

        }
    }

}
