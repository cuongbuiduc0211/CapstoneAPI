using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class EngineType
    {
        public EngineType()
        {
            Attributions = new HashSet<Attribution>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Attribution> Attributions { get; set; }
    }
}
