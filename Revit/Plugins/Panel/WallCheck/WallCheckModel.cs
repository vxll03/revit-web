using System;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;

namespace Plugins.Panel.WallCheck;

public class WallCheckModel : IRevitPluginModel
{
    public event Action<MessageToWeb> OnMessageReady;
    public IPayload Payload { get; set; } = new BasePayload();
    private readonly ActionEventHandler _handler;

    public WallCheckModel(ActionEventHandler handler)
    {
        _handler = handler;
    }

    public void Execute()
    {
        _handler.Raise(WallCheckEventHandler.CheckWalls);

        var msg = new MessageToWeb("Плагин выполнен", "Проблем не было 💩");
        OnMessageReady?.Invoke(msg);
    }
}