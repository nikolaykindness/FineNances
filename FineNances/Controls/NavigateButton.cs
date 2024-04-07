using System.Windows;
using System.Windows.Controls;

namespace FineNances.Controls
{
    internal class NavigateButton : RadioButton
    {
        static NavigateButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateButton), new FrameworkPropertyMetadata(typeof(NavigateButton)));
        }
    }
}
