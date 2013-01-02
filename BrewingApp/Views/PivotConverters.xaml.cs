using Microsoft.Phone.Controls;
using BrewBuddy.ViewModels;

namespace BrewBuddy.Pages
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