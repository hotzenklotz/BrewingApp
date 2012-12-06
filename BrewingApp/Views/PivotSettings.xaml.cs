using Microsoft.Phone.Controls;
using BrewingApp.ViewModels;

namespace BrewingApp.Views
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            this.DataContext = new SettingsVM();
            InitializeComponent();
            
        }
    }
}