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
            Settings.Initialize();
           
        }

        // FIX ME background of tile visible
        // http://blog.humann.info/post/2011/11/06/Silverlight-Toolkit-bug-on-HubTile-control-the-solution.aspx
        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri page;
            HubTile tile = (HubTile) sender;

            switch (tile.Name)
            {
                case "BrewHouseTile":
                    page = new Uri("/Views/PivotBrewHouse.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "HopsTile":
                    page = new Uri("/Views/PivotHops.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "ConvertersTile":
                    page = new Uri("/Views/PivotConverters.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "MaltsTile":
                    page = new Uri("/Views/PivotMalts.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "SettingsTile":
                    page = new Uri("/Views/Settings.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default :
                    page = new Uri("");
                    break;
            }

            NavigationService.Navigate(page);

        }


    }
}