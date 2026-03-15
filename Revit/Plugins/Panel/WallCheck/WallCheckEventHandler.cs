using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Plugins.Panel.WallCheck;

internal sealed class WallCheckEventHandler
{
    public static void CheckWalls(UIApplication app)
    {
        var uidoc = app.ActiveUIDocument;
        if (uidoc is null) return;

        var doc = uidoc.Document;
        var activeView = doc.ActiveView;
        if (activeView is null) return;

        var wallIds = new FilteredElementCollector(doc, activeView.Id)
            .OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType()
            .ToElementIds();

        if (wallIds.Any())
        {
            uidoc.Selection.SetElementIds(wallIds);
        }
        else
        {
            TaskDialog.Show("Результат", "Стены на текущем виде не найдены.");
        }
    }

    public string GetName() => "SelectWallsHandler";
}