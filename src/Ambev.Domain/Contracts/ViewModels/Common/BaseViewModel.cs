using System.Text.Json.Serialization;

namespace Ambev.Domain.Contracts.ViewModels.Common
{
    public abstract class BaseViewModel
    {
        [JsonPropertyOrder(-1)]
        public Guid Id { get; set; }
    }
}
