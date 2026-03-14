using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebBridge.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly Dictionary<string, IRevitPluginModel> _models = new Dictionary<string, IRevitPluginModel>();

        public void RegisterModel(string name, IRevitPluginModel model)
        {
            if (!_models.ContainsKey(name))
            {
                _models.Add(name, model);
            }
        }

        public IEnumerable<IRevitPluginModel> GetRegisteredModels()
        {
            return _models.Values.ToList();
        }

        protected override void OnCommandReceived(JToken payload)
        {
            var pluginName = payload?["plugin"]?.ToString();
            if (string.IsNullOrEmpty(pluginName))
            {
                MessageBox.Show("Ошибка: в команде не указан 'plugin'.", "ViewModel");
                return;
            }

            
            if (_models.TryGetValue(pluginName, out IRevitPluginModel model))
            {
                JsonConvert.PopulateObject(payload.ToString(), model.Payload);
                model.Execute();
            }
            else
                MessageBox.Show($"Ошибка: плагин '{pluginName}' не найден.", "ViewModel");
        }
    }
}