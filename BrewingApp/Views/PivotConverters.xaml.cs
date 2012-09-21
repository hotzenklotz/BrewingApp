using Microsoft.Phone.Controls;
using BrewingApp.ViewModels;

namespace BrewingApp.Pages
{
    public partial class PivotConverters : PhoneApplicationPage
    {

        public PivotConverters()
        {
            InitializeComponent();

            DataContext = new UnitConversionVM();

        }
    }
}   