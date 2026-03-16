using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;

namespace Plugins.Panel.IntersectionCheck;

public class IntersectionCheckModel : BasePluginModel
{
    public IntersectionCheckModel(AsyncEventHandler handler) : base(handler) { }

    protected override async Task HandlePlugin()
    {
        SendToast("Старт", "Собираем данные модели...");
        var exportData = new List<string>();

        await RunInRevitAsync(app => 
        {
            var doc = app.ActiveUIDocument.Document;

        });
        
        SendToast("Сеть", "Отправляем данные в Бекенд");
        
        var backendResponse = await SendToFastApiAsync(exportData);

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

    private async Task<string> SendToFastApiAsync(object data)
    {
        await Task.Delay(2000);
        return "Success";
    }
}