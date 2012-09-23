using Microsoft.Phone.Controls;
using BrewingApp.ViewModels;

namespace BrewingApp.Pages
{
    public partial class PivotConverters : PhoneApplicationPage
    {

        public PivotConverters()
        {
            this.DataContext = new UnitConversionVM();

            InitializeComponent();

        }
    }
}   