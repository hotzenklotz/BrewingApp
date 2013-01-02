using System;
using BrewBuddy.Other;
using BrewBuddy.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;

namespace BrewBuddy.Pages
{
    public partial class PivotMalts : PhoneApplicationPage
    {
        public PivotMalts()
        {
            InitializeComponent();

            this.GravityPivot.DataContext = new GravityVM();
            this.InfusionPivot.DataContext = new InfusionVM();

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