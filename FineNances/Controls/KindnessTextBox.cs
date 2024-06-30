using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace FineNances.Controls
{
    internal class KindnessTextBox : TextBox
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(KindnessTextBox), new PropertyMetadata(default(string)));

        private Color textWritingColor;
        private Color placeholderColor;

        private bool isWriting = false;

        static KindnessTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KindnessTextBox), new FrameworkPropertyMetadata(typeof(KindnessTextBox)));
        }

        public KindnessTextBox()
        {
            BorderThickness = new Thickness(0);
            textWritingColor = (Color)ColorConverter.ConvertFromString("#1E1E1E");
            placeholderColor = (Color)ColorConverter.ConvertFromString("#B1B1B1");
        }

        private void KindnessTextBox_Initialized(object sender, System.EventArgs e)
        {
            Foreground = new SolidColorBrush(placeholderColor);
            Text = Placeholder;
        }

        public void RemoveText(object sender, RoutedEventArgs e)
        {
            if (Text == Placeholder && !isWriting)
            {
                Foreground = new SolidColorBrush(textWritingColor);
                Text = "";
                isWriting = true;
            }
        }

        public void AddText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Foreground = new SolidColorBrush(placeholderColor);
                Text = Placeholder;
                isWriting = false;
            }
        }
    }
}
