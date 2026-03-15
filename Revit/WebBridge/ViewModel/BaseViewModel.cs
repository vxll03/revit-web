using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebBridge.ViewModel
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        public event Action<string> OnSendMessageToWeb;
        
        [RelayCommand]
        public virtual void HandleWebMessage(string json)
        {
            try
            {
                var doc = JObject.Parse(json);
                OnCommandReceived(doc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected abstract void OnCommandReceived(JToken payload);
        
        protected void SendToWeb(string action, object data)
        {
            var response = new 
            {
                action = action,
                payload = data
            };
            
            string jsonResponse = JsonConvert.SerializeObject(response);
            OnSendMessageToWeb?.Invoke(jsonResponse);
        }
    }
}