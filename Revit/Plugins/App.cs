using System;
using System.Reflection;
using Autodesk.Revit.UI;
using Plugins.Panel.MainPlugin;

namespace Plugins
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "My Plugins";
            application.CreateRibbonTab(tabName);
            var panel = application.CreateRibbonPanel(tabName, "Commands");
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            var helloWorldButtonData = new PushButtonData(
                "HelloWorld",
                "Show WebView",
                assemblyPath,
                "Plugins.Panel.MainPlugin.Command"
            );
            
            panel.AddItem(helloWorldButtonData);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}