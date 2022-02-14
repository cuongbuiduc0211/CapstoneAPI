using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class CarModel
    {
        public CarModel()
        {
            Generations = new HashSet<Generation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Generation> Generations { get; set; }
    }
}
