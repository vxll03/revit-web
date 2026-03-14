using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;

namespace Plugins.Panel.MainPlugin
{
    public class Model : IRevitPluginModel, IRevitInitializable
    {
        private UIApplication _uiApp;
        private ExternalEvent _externalEvent;
        
        public IPayload Payload { get; set; }

        public void Initialize(UIApplication uiApp)
        {
            Payload = new BasePayload();
            _uiApp = uiApp;
            _externalEvent = ExternalEvent.Create(new Handler());
        }


        public void Execute()
        {
            _externalEvent?.Raise();
        }
    }
}