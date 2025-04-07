using Ambev.Shared.Common.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ambev.Api.Models.Binders
{
    public class DynamicQueryParametersBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var query = bindingContext.HttpContext.Request.Query;
            var result = new Dictionary<string, string>();

            HashSet<string> fixedKeys = new();

            var queryParametersProperties = typeof(QueryParameters).GetProperties();

            foreach (var parameter in queryParametersProperties) 
            {
                var parameterNameUnderscored = "_" + parameter.Name;
                fixedKeys.Add(parameterNameUnderscored.ToLowerInvariant());
                fixedKeys.Add(parameter.Name.ToLowerInvariant());
            }

            foreach (var kvp in query)
            {

                if (!fixedKeys.Contains(kvp.Key.ToLowerInvariant()))
                {
                    result[kvp.Key] = kvp.Value.ToString();
                }
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
