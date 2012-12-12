using Microsoft.Phone.Controls;
using BrewingApp.ViewModels;

namespace BrewingApp.Views
{
    public partial class PivotSettings : PhoneApplicationPage
    {
        public PivotSettings()
        {
            InitializeComponent();
            this.DataContext = new SettingsVM();
        }
    }
}