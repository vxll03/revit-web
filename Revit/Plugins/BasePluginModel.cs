using System;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Core.Contracts;
using Core.Models;
using Nice3point.Revit.Toolkit.External.Handlers;

namespace Plugins;

public abstract class BasePluginModel : IRevitPluginModel
{
    public event Action<MessageToWeb> OnMessageReady;
    public virtual IPayload Payload { get; set; } = new BasePayload();
    protected readonly AsyncEventHandler Handler;
    
    protected BasePluginModel(AsyncEventHandler handler)
    {
        Handler = handler;
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

    protected async Task RunInRevitAsync(Action<UIApplication> action)
    {
        await Handler.RaiseAsync(action);
    }

    protected void SendToast(string title, string message)
    {
        OnMessageReady?.Invoke(new MessageToWeb(title, message));
    }
}