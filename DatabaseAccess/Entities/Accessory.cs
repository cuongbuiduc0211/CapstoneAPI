using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Accessory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
