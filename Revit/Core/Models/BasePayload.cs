using Core.Contracts;

namespace Core.Models
{
    /// <summary>
    /// Basic implementation of IPayload interface
    /// </summary>
    public class BasePayload: IPayload
    {
        public string Command { get; set; }
    }
}