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
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using BrewingApp.Models;

namespace BrewingApp.ViewModels
{
    public class SubstitutionVM : ViewModelBase
    {
        private string _Selection1;
        private string _Selection2;
        private float _AlphaAcid1;
        private float _AlphaAcid2;
        private float _Amount1;
        private float _Amount2;

        public Dictionary<string, Hop> HopVarities { get; set; }

        #region Public Properties

        public string HopSelection1
        { 
            get { return this._Selection1; }
            set {
                this._Selection1 = value;
                AlphaAcid1 = this.HopVarities[value].AlphaAcid;
                RaisePropertyChanged("AlphaAcid1");
            }
        }

        public string HopSelection2
        {
            get { return this._Selection2; }
            set
            {
                this._Selection2 = value;
                AlphaAcid2 = this.HopVarities[value].AlphaAcid;
                RaisePropertyChanged("AlphaAcid2");
            }
        }

        public float AlphaAcid1 
        {
            get { return this._AlphaAcid1; }
            set 
            { 
                this._AlphaAcid1 = value;
                CalculateSubstitute();
            }
        }

        public float AlphaAcid2
        {
            get { return this._AlphaAcid2; }
            set
            {
                this._AlphaAcid2 = value;
                CalculateSubstitute();
            }
        }

        public float Amount1
        {
            get { return this._Amount1; }
            set 
            {
                this._Amount1 = value;
                CalculateSubstitute();
            }    
        }

        public float Amount2
        {
            get { return this._Amount2;  }
            set { this._Amount2 = value; }
        }

        #endregion

        private void CalculateSubstitute()
        {
            this._Amount2 = (float) Math.Round(AlphaAcid1 * Amount1 / AlphaAcid2,2);
            RaisePropertyChanged("Amount2");
        }


        public SubstitutionVM()
        {
            this.HopVarities = Hop.loadHopVarities();
            this._Selection1 = this.HopVarities.Keys.First();
            this._Selection2 = this.HopVarities.Keys.Last();

            AlphaAcid1 = this.HopVarities[this._Selection1].AlphaAcid;
            AlphaAcid2 = this.HopVarities[this._Selection2].AlphaAcid;

            RaisePropertyChanged("AlphaAcid1");
            RaisePropertyChanged("AlphaAcid2");
            RaisePropertyChanged("HopSelection1");
            RaisePropertyChanged("HopSelection2");

        }

    }
}
