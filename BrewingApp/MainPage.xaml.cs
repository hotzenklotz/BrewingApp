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
using BrewingApp.Models;

namespace BrewingApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            new Settings();
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri page;
            HubTile tile = (HubTile)sender;

            switch (tile.Name)
            {
                case "BrewHouse":
                    page = new Uri("/Views/PivotBrewHouse.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "HopsMalt":
                    page = new Uri("/Views/PivotHopsMalt.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Converters":
                    page = new Uri("/Views/PivotConverters.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Mashing":
                    page = new Uri("/Views/PivotMashing.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default :
                    page = new Uri("");
                    break;
            }

            NavigationService.Navigate(page);

        }


    }
}