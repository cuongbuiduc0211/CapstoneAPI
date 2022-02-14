using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class GenerationAttribution
    {
        public string Id { get; set; }
        public string GenerationId { get; set; }
        public string AttributionId { get; set; }
        public string Value { get; set; }

        public virtual Attribution Attribution { get; set; }
        public virtual Generation Generation { get; set; }
    }
}
