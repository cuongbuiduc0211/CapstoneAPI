using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class District
    {
        public District()
        {
            Exchanges = new HashSet<Exchange>();
            Wards = new HashSet<Ward>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Exchange> Exchanges { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
