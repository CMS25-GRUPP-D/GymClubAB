using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ViewModels;
using Presentation.Views;

namespace Presentation;


public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<MemberAddViewModel>();
            services.AddTransient<MemberAddView>();

        })
   
            .Build();

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainViewModel mainViewModel = _host!.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host!.Services.GetRequiredService<MemberAddViewModel>();

        MainWindow mainWindow = _host!.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = mainViewModel;

        mainWindow.Show();
    }
}
