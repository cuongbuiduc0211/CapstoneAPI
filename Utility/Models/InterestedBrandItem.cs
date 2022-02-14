using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class InterestedBrandItem
    {
        public int UserId { get; set; }
        public List<UserInterestedBrand> UserInterestedBrands { get; set; }       
    }
    public class UserInterestedBrand
    {
        public string BrandId { get; set; }
        public InterestedBrandStatus Status { get; set; }
    }


}
