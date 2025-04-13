using System.Text.Json.Serialization;

namespace Ambev.Application.Contracts.ViewModels.Common
{
    public abstract class BaseViewModel
    {
        [JsonPropertyOrder(-1)]
        public Guid Id { get; set; }
    }
}
