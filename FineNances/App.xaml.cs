using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace FineNances
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    new CultureInfo("ru-RU").IetfLanguageTag)));
        }
    }
}
