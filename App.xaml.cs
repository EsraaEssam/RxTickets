using ReactiveUI.Builder;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RxTickets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _ = RxAppBuilder.CreateReactiveUIBuilder()
                .WithWpf()
                .BuildApp();
        }
    }

}
