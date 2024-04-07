using System.Windows;
using System.Windows.Controls;

namespace FineNances.Controls
{
    /// <summary>
    /// Interaction logic for KindnessModalDialog.xaml
    /// </summary>
    public partial class KindnessModalDialog : UserControl
    {


        static KindnessModalDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KindnessModalDialog), new FrameworkPropertyMetadata(typeof(KindnessModalDialog)));
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(KindnessModalDialog), new PropertyMetadata(default(bool)));

    }
}
