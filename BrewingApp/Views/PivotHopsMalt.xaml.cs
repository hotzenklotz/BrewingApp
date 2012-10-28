using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using BrewingApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using BrewingApp.Other;

namespace BrewingApp.Pages
{
    public partial class PivotHopsMalt : PhoneApplicationPage
    {
        public PivotHopsMalt()
        {
            InitializeComponent();

            this.BitternessPivot.DataContext = new BitternessVM();
            this.GravityPivot.DataContext = new GravityVM();

            Messenger.Default.Register<NavigateMessage>
            (
                this,
                (message) => NavigateToPage(message.PageName)
            );
        }

        private void NavigateToPage(string pageName)
        {
            NavigationService.Navigate(new Uri(pageName,UriKind.RelativeOrAbsolute));
        }
            
    }
}