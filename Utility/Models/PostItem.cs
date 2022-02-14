using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class PostItem
    {
        [Required]
        public PostType Type { get; set; }
        [Required]
        public string Title { get; set; }
        public string FeaturedImage { get; set; }
        [Required]
        public string Overview { get; set; }
        [Required]
        public string Contents { get; set; }
        public string BrandId { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public PostStatus Status { get; set; }
    }
}
