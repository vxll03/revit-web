using System;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;

namespace Plugins.Common;

public abstract class BasePluginModel : IRevitPluginModel
{
    public event Action<MessageToWeb> OnMessageReady;
    public virtual IPayload Payload { get; set; } = new BasePayload();
    private readonly AsyncEventHandler _handler;
    
    protected BasePluginModel(AsyncEventHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Execute()
    {
        try
        {
            await HandlePlugin();
        }
        catch (Exception ex)
        {
            SendToast("Ошибка выполнения", ex.Message);
        }
    }

    protected abstract Task HandlePlugin();

    protected async Task RunInRevitAsync(Action<UIApplication> func)
    {
            await _handler.RaiseAsync(func);
    }

    protected void SendToast(string title, string message)
    {
        OnMessageReady?.Invoke(new MessageToWeb(title, message));
    }
}