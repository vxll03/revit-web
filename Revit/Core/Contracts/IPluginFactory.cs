namespace Core.Contracts;

public interface IPluginFactory
{
    IRevitPluginModel Create(string pluginName);
}