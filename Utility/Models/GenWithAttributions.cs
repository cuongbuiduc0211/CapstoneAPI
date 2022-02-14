using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class GenWithAttributions
    {
        public string GenerationId { get; set; }
        public List<AttributionWithValue> AttributionWithValues { get; set; }
        
    }
    public class AttributionWithValue
    {
        public string AttributionId { get; set; }
        public string Value { get; set; }
    }
}
