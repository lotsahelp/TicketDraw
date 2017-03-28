using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicketDraw.View
{
    /// <summary>
    /// Interaction logic for DrawControl.xaml
    /// </summary>
    public partial class DrawControl : UserControl
    {
        public DrawControl()
        {
            InitializeComponent();
        }

        public class StringNullOrEmptyToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var s = value as string;
                return string.IsNullOrEmpty(s) ? Visibility.Collapsed : Visibility.Visible;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return null;
            }
            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                return this;
            }
        }
    }
}
