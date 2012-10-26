using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Models;
using BrewingApp.Other;
using System;
using BrewingApp.Converters;

namespace BrewingApp.ViewModels
{
    public class GravityVM : MaltHopBase<Malt> 
    {
       
        private int _Gravity;
        private float _BatchVolume;

        private WeightConverter _Converter;

        #region public properties

        public float BatchVolume 
        {
            get { return this._BatchVolume; }
            set { this._BatchVolume = value; calculateGravity(); }
        }

        public int Gravity 
        {
            get { return this._Gravity; }
            set { this._Gravity = value; RaisePropertyChanged("Gravity"); }
        }

        #endregion

        public GravityVM() : base()
        {
            this._Converter = new WeightConverter();

            //add at least one item by default to fill the list
            ItemList = new ObservableCollection<Malt>();
            ItemList.Add(new Malt());

            //default Values
            BatchVolume = 20;

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
            Messenger.Default.Send<NavigateMessage>(new NavigateMessage("/Views/EditHop.xaml"));
        }

        /// <summary>
        /// Calculates the original gravity of a sud based on the provided malts and adjuncts
        /// Formula : http://www.brewersfriend.com
        /// </summary>
        /// <returns></returns>
        private int calculateGravity() 
        {
            float gravity = 0.0f;
            float maltGravity = 0.0f;
            float adjunctGravity = 0.0f;//not affeted by boilding efficiency (usually added later)

            foreach (Malt item in ItemList)
            {
                if (item.isMashable)
                    maltGravity += item.PPG * this._Converter.Convert(item.Amount, "Pound");
                else
                    adjunctGravity += item.PPG * this._Converter.Convert(item.Amount, "Pound");
            }

            maltGravity *= (efficiency / 100);
            gravity = maltGravity + adjunctGravity; 
            gravity /= this._Converter.Convert(this._BatchVolume, "US Gallons");

            //output range 1.000 - 1.1XX
            return (int) (gravity * 0.001 + 1);

        }
    }

}
