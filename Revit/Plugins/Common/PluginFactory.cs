using System;
using Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Plugins.Common;

public class PluginFactory: IPluginFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PluginFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRevitPluginModel Create(string pluginName)
    {
        return _serviceProvider.GetKeyedService<IRevitPluginModel>(pluginName);
    }
}
