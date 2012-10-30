using System;
using BrewingApp.Other;
using BrewingApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;

namespace BrewingApp.Pages
{
    public partial class PivotMalts : PhoneApplicationPage
    {
        public PivotMalts()
        {
            InitializeComponent();

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