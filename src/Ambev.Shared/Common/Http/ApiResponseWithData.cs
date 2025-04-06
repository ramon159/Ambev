using System.Text.Json.Serialization;

namespace Ambev.Shared.Common.Http
{
    public class ApiResponseWithData<T> : ApiResponse
    {
        [JsonPropertyOrder(-1)]
        public T? Data { get; set; }
    }
}
