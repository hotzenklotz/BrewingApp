using Microsoft.Phone.Controls;
using BrewBuddy.ViewModels;

namespace BrewBuddy.Views
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