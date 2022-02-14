using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Prize
    {
        public Prize()
        {
            ContestPrizes = new HashSet<ContestPrize>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<ContestPrize> ContestPrizes { get; set; }
    }
}
