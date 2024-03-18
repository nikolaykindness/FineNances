using System.Windows.Controls;
using System.Windows;

namespace FineNances.Core
{
    internal class NavigateButton : RadioButton
    {
        static NavigateButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateButton), new FrameworkPropertyMetadata(typeof(NavigateButton)));
        }
    }
}
