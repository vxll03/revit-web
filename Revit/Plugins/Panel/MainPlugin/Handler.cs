using Autodesk.Revit.UI;

namespace Plugins.Panel.MainPlugin
{
    public class Handler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            TaskDialog.Show("Event Handler", "This is the event handler logic.");
        }

        public string GetName()
        {
            return "HelloWorldPlugin Event Handler";
        }
    }
}