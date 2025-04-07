using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Shared.Models
{
    public sealed class SortField
    {
        public string Field { get; set; } = string.Empty;
        public bool Ascending { get; set; } = true;
    }
}
