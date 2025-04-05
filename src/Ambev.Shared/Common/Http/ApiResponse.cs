using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Shared.Common.Http
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        //public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
    }
}
