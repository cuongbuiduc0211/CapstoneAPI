using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Attribution
    {
        public Attribution()
        {
            GenerationAttributions = new HashSet<GenerationAttribution>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string RangeOfValue { get; set; }
        public string Measure { get; set; }
        public int Type { get; set; }
        public string EngineType { get; set; }

        public virtual EngineType EngineTypeNavigation { get; set; }
        public virtual ICollection<GenerationAttribution> GenerationAttributions { get; set; }
    }
}
