using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;
using Plugins.Common;
using Plugins.Panel.WallCheck;

namespace Plugins.Panel.IntersectionCheck;

public class IntersectionCheckModel : BasePluginModel
{
    public IntersectionCheckModel(AsyncEventHandler handler) : base(handler) { }

    protected override async Task HandlePlugin()
    {
        SendToast("Старт", "Собираем данные модели...");
        List<Element> viewElements = new();
        await RunInRevitAsync(app =>
        {
            viewElements = WallCheckEventHandler.GetWallsFromActiveView(app);
        });
        SendToast("Сеть", viewElements[0].LevelId.ToString());
        await Task.Delay(2000);
        

        await RunInRevitAsync(app => 
        {
            var doc = app.ActiveUIDocument.Document;
            using var tx = new Transaction(doc, "Применение расчетов");
            tx.Start();
            // Мутация
            tx.Commit();
        });

        SendToast("Готово", "Модель успешно обновлена!");
    }
}