using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Post
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string FeaturedImage { get; set; }
        public string Overview { get; set; }
        public string Contents { get; set; }
        public string BrandId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
    }
}
