namespace Ambev.Application.Models.Http
{
    public class ApiValidationProblemDetails : ApiErrorResponse
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public ApiValidationProblemDetails(IDictionary<string, string[]> errors)
        {
            Errors=errors;
        }
    }
}
