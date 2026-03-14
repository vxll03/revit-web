using System;
using System.IO;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using WebBridge.ViewModel;

namespace WebBridge.View
{
    public partial class WebView : Window
    {
        private string _uri;
        
        public WebView(BaseViewModel vm, string uri)
        {
            InitializeComponent();
            DataContext = vm;
            InitializeBrowser(vm);
            _uri = uri;
        }

        private async void InitializeBrowser(BaseViewModel vm)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string userDataFolder = Path.Combine(localAppData, "RevitPlugins", "WebView2_Cache");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
    
            await webView.EnsureCoreWebView2Async(environment);
            webView.Source = new Uri(_uri);

            webView.CoreWebView2.WebMessageReceived += (sender, args) =>
            {
                var json = args.WebMessageAsJson;
                vm.HandleWebMessageCommand.Execute(json);
            };
            
            vm.OnSendMessageToWeb += (json) =>
            {
                if (webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.PostWebMessageAsJson(json);
                }
            };
        }
    }
}