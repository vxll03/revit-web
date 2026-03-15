using System.Reflection;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;

namespace Plugins
{
    public class App : ExternalApplication
    {
        public override void OnStartup()
        {
            string tabName = "MyPlugins";
            Application.CreateRibbonTab(tabName);
            var panel = Application.CreateRibbonPanel(tabName, "Commands");
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            var helloWorldButtonData = new PushButtonData(
                "HelloWorld",
                "Show WebView",
                assemblyPath,
                "Plugins.Command"
            );

            panel.AddItem(helloWorldButtonData);
        }
    }
}