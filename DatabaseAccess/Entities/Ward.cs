using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Ward
    {
        public Ward()
        {
            Exchanges = new HashSet<Exchange>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string DistrictId { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<Exchange> Exchanges { get; set; }
    }
}
