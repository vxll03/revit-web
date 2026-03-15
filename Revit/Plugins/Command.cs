using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Nice3point.Revit.Toolkit.External;
using Nice3point.Revit.Toolkit.External.Handlers;
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
        
        _serviceProvider.GetRequiredService<ActionEventHandler>();
        
        var windowManager = _serviceProvider.GetRequiredService<WindowManager>();
        windowManager.ShowOrActivate();
    }

    private IServiceProvider BuildDi()
    {
        var services = new ServiceCollection();

        // Revit context init
        services.AddSingleton(Application);
        services.AddSingleton<ActionEventHandler>();
        
        // ViewModel & View init
        services.AddTransient<MainViewModel>();
        services.AddSingleton<WindowManager>();

        // Plugins init
        services.AddSingleton<IPluginFactory, PluginFactory>();
        services.AddKeyedTransient<IRevitPluginModel, WallCheckModel>("WallCheck");
        
        return services.BuildServiceProvider();
    }
}