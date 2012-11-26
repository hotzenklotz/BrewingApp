using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Models;
using BrewingApp.Other;
using System.Linq;
using System;
using BrewingApp.Converters;

namespace BrewingApp.ViewModels
{
    public class BitternessVM : MaltHopBase<Hop> 
    {
       
        private int _IBU;
        private float _BatchVolume;
        private float _SpecificGravity;

        #region public properties

        public float SpecificGravity 
        {
            get { return this._SpecificGravity; }
            set { this._SpecificGravity = value; calculateIBU(); }
        }

        public float BatchVolume 
        {
            get { return this._BatchVolume; }
            set { this._BatchVolume = value; calculateIBU(); }
        }

        public int IBU 
        {
            get { return this._IBU; }
            set { this._IBU = value; RaisePropertyChanged("IBU"); }
        }

        #endregion

        public BitternessVM() : base()
        {
            //add at least one item by default to fill the list
            ItemList = new ObservableCollection<Hop>();

            Hop firstItem = new Hop();
            setDefaultValues(firstItem);

            ItemList.Add(firstItem);

            //default Values
            BatchVolume = VolumeConverter.Convert(20);
            SpecificGravity = 1.010f;

            //use MVVM LightToolkit messaging for custom controls update notifications
            Messenger.Default.Register<UpdateViewMessage>
                       (
                           this,
                           (message) =>
                           {
                               if (message.ViewName == "BitternessVM")
                                   updateView();
                               calculateIBU();
                           }
                       );
        }

        public override void editItem(Hop item) 
        {
            base.editItem(item);
            Messenger.Default.Send<NavigateMessage>(new NavigateMessage("/Views/EditHop.xaml"));
        }

        public override void setDefaultValues(Hop item)
        {
            item.Name = Hop.loadHopVarities().Values.First().Name;
            item.AlphaAcid = Hop.loadHopVarities().Values.First().AlphaAcid;
            item.BoilTime = 60;
            item.Amount = WeightConverter.Convert(10);
        }

        /// <summary>
        /// Calculates the IBU (international bitterness units) based on the amount of hops, their boil time and alpha acids
        /// Formulas based on the work of Rager / Tinseth
        /// http://www.rockhoppersbrewclub.com/wiki/calculating-ibus
        /// </summary>
        private void calculateIBU()
        {

            //TODO use boild gravity instead of original gravity???
            //TODO support pelets? efficiency * 1.1
            string formula = Settings.HopFormula;
            float utilization = 0.0f;
            float ibu = 0.0f;

            IBU = 0;
       
            switch ( formula )
            {
                case "Rager" :

                    float gravityAdjustment;

                    if (SpecificGravity > 1.050)
                    {
                        gravityAdjustment = (SpecificGravity - 1.050f) / 0.2f;
                    }
                    else {
                        gravityAdjustment = 0.0f;
                    }

                    foreach (Hop hop in ItemList)
                    {
                        utilization = 0.1811f + 0.1386f * (float) Math.Tanh(( hop.BoilTime - 31.32f) / 18.27f);
                        ibu += hop.Amount * hop.AlphaAcid * utilization * 10 / BatchVolume * (1 + gravityAdjustment);
                    }                 

                    break;

                case "Tinseth" :

                    float alphaAcids;
                    float bignessFactor;
                    float boilTimeFactor;


                    foreach (Hop hop in ItemList)
                    {
                        alphaAcids = (hop.AlphaAcid * 0.01f * hop.Amount * 1000.0f) / BatchVolume;

                        bignessFactor = 1.65f * (float) Math.Pow( 0.000125f, (SpecificGravity - 1.0f));
                        boilTimeFactor = ( 1.0f - (float) Math.Pow( Math.E, (-0.04f * hop.BoilTime))) / 4.15f;

                        utilization = bignessFactor * boilTimeFactor;
                        ibu += alphaAcids * utilization;
                    }
                    break;

                default:
                    break;

            }

            IBU = (int) Math.Round(ibu);
        }
    }

}
