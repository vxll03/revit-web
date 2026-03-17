using System;
using System.Diagnostics;
using System.Windows.Interop;
using Microsoft.Extensions.DependencyInjection;
using WebBridge.View;
using WebBridge.ViewModel;

namespace Plugins.Common;

public class WindowManager
{
    private const string MAIN_PAGE_URL = "localhost:3000/main";
    private WebView _window;
    private readonly IServiceProvider _serviceProvider;

    public WindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowOrActivate()
    {
        if (_window == null || !_window.IsLoaded)
        {
            var viewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            _window = new WebView(viewModel, MAIN_PAGE_URL);

            var helper = new WindowInteropHelper(_window)
            {
                Owner = Process.GetCurrentProcess().MainWindowHandle
            };

            _window.Show();
        }
        else
        {
            _window.Activate();
        }
    }
}