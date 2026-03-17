using System;
using Autodesk.Revit.Attributes;
using Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Nice3point.Revit.Toolkit.External;
using Nice3point.Revit.Toolkit.External.Handlers;
using Plugins.Common;
using Plugins.Panel.IntersectionCheck;
using Plugins.Panel.WallCheck;
using WebBridge.ViewModel;

namespace Plugins;

/// <summary>
/// Main plugins command that initializes every plugin and give it to ViewModel.
///
/// Every new plugin needs to be initialized in BuildDi method as Singleton or Transient
/// </summary>
[Transaction(TransactionMode.Manual)]
public class Command : ExternalCommand
{
    private static IServiceProvider _serviceProvider;

    public override void Execute()
    {
        _serviceProvider ??= BuildDi();
        
        _serviceProvider.GetRequiredService<AsyncEventHandler>();
        
        var windowManager = _serviceProvider.GetRequiredService<WindowManager>();
        windowManager.ShowOrActivate();
    }

    private IServiceProvider BuildDi()
    {
        var services = new ServiceCollection();

        // Revit context init
        services.AddSingleton(Application);
        services.AddSingleton<AsyncEventHandler>();
        
        // ViewModel & View init
        services.AddTransient<MainViewModel>();
        services.AddSingleton<WindowManager>();

        // Plugins init
        services.AddSingleton<IPluginFactory, PluginFactory>();
        services.AddKeyedTransient<IRevitPluginModel, WallCheckModel>("WallCheck");
        services.AddKeyedTransient<IRevitPluginModel, IntersectionCheckModel>("IntersectionCheck");
        
        return services.BuildServiceProvider();
    }
}