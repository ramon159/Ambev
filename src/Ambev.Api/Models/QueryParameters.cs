using Ambev.Api.Models.Binders;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Models
{
    /// <summary>
    /// Filtering, sorting and paging parameters
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// Current page, default 1
        /// </summary>
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;
        /// <summary>
        /// Size of page, default 10.
        /// </summary>
        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;

        /// <summary>
        /// Sort order.
        /// a string of fields to sort by, optionally followed by "asc" or "desc", default "asc".
        /// For example: ?_order="price desc, title asc"
        /// </summary>
        /// 
        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;

        /// <summary>
        /// Filter parameters 
        /// For examples: _minPrice=50
        /// and _minDate=2023-01-01
        /// and category=*clothing
        /// </summary>
        [ModelBinder(BinderType = typeof(DynamicQueryParametersBinder))]
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
    }
}
