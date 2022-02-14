using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string FeaturedImage { get; set; }

        public string Title { get; set; }
        public string Overview { get; set; }
        public string Contents { get; set; }
        public int Type { get; set; }
        public string BrandId { get; set; }
        public string BrandImage { get; set; }
        public string BrandName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorImage { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
