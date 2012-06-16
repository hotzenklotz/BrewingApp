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

namespace BrewingApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri page;
            HubTile tile = (HubTile)sender;

            switch (tile.Name)
            {
                case "BrewHouse":
                    page = new Uri("/Pages/PivotBrewHouse.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "HopsMalt":
                    page = new Uri("/Pages/PivotHopsMalt.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Converters":
                    page = new Uri("/Pages/PivotConverters.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Mashing":
                    page = new Uri("/Pages/PivotMashing.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default :
                    page = new Uri("");
                    break;
            }

            NavigationService.Navigate(page);

        }


    }
}