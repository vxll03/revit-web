using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Core.Contracts;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebBridge.ViewModel;

public partial class MainViewModel : BaseViewModel
    {
        private readonly IPluginFactory _pluginFactory;
        
        public MainViewModel(IPluginFactory pluginFactory)
        {
            _pluginFactory = pluginFactory;
        }

        protected override void OnCommandReceived(JToken payload)
        {
            var pluginName = payload?["plugin"]?.ToString();
            
            if (string.IsNullOrEmpty(pluginName))
            {
                MessageBox.Show("Ошибка: в команде не указан 'plugin'.", "ViewModel");
                return;
            }
            
            var model = _pluginFactory.Create(pluginName);

            if (model != null)
            {
                model.OnMessageReady += (msg) => SendToWeb("toast", msg);
                JsonConvert.PopulateObject(payload.ToString(), model.Payload);
                model.Execute();
            }
            else
            {
                MessageBox.Show($"Ошибка: плагин '{pluginName}' не найден.", "ViewModel");
            }
        }
    }