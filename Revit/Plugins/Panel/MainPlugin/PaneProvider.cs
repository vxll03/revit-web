using Autodesk.Revit.UI;
using WebBridge.View;
using WebBridge.ViewModel;

namespace Plugins.Panel.MainPlugin
{
    public class PaneProvider : IDockablePaneProvider
    {
        internal static MainViewModel ViewModel;

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            ViewModel = new MainViewModel();
            var view = new WebView(ViewModel, "https://google.com");
            
            var helloWorldModel = new Model();
            ViewModel.RegisterModel("HelloWorld", helloWorldModel);

            view.DataContext = ViewModel;

            data.FrameworkElement = view;
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right
            };
        }
    }
}