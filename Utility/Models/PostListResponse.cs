using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class PostListResponse
    {
        public int Id { get; set; }
        public string FeaturedImage { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int Type { get; set; }
        public string CreatorImage { get; set; }
        public string CreatorName { get; set; }
        public string BrandId { get; set; }
        public int Status { get; set; }
    }
}
