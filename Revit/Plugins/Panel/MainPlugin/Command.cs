using System.Diagnostics;
using System.Windows.Interop;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using WebBridge.View;
using WebBridge.ViewModel;

namespace Plugins.Panel.MainPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private static WebView _window;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (_window == null || !_window.IsLoaded)
            {
                var viewModel = new MainViewModel();
                _window = new WebView(viewModel, "https://google.com");
                
                var helloWorldModel = new Model();
                helloWorldModel.Initialize(commandData.Application);
                
                viewModel.RegisterModel("HelloWorld", helloWorldModel);
                _window.DataContext = viewModel;

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

            return Result.Succeeded;
        }
    }
}