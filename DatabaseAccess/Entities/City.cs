using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
            Exchanges = new HashSet<Exchange>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Exchange> Exchanges { get; set; }
    }
}
