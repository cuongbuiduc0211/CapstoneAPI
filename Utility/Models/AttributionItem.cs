using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class AttributionItem
    {
        public string Name { get; set; }
        public string RangeOfValue { get; set; }
        public string Measure { get; set; }
        public AttributionType Type { get; set; }
        public string EngineType { get; set; }
    }
}
