using Autodesk.Revit.UI;

namespace Plugins;

public interface IRevitInitializable
{
    void Initialize(UIApplication app);
}