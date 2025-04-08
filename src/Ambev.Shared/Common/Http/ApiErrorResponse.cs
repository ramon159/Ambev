namespace Ambev.Shared.Common.Http
{
    public class ApiErrorResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
