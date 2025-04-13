using System.Text.Json.Serialization;

namespace Ambev.Application.Models.Http
{
    public class ApiResponseWithData<T> : ApiResponse
    {
        [JsonPropertyOrder(-1)]
        public T? Data { get; set; }
    }
}
