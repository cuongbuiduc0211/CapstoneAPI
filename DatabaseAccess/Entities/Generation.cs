using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Generation
    {
        public Generation()
        {
            GenerationAttributions = new HashSet<GenerationAttribution>();
        }

        public string Id { get; set; }
        public string CarModelId { get; set; }
        public string Name { get; set; }
        public int YearOfManufactor { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }

        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<GenerationAttribution> GenerationAttributions { get; set; }
    }
}
