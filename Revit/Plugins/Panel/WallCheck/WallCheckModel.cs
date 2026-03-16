using System;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;

namespace Plugins.Panel.WallCheck;

public class WallCheckModel : BasePluginModel
{ 
    public WallCheckModel(AsyncEventHandler handler) : base(handler) { }
    
    protected override async Task HandlePlugin()
    {
        await RunInRevitAsync(WallCheckEventHandler.CheckWalls);
        SendToast("Плагин выполнен", "Ошибок не найдено");
    }
}