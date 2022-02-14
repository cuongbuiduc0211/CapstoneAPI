using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class ContestPrizeItem
    {
        public string ContestId { get; set; }
        public string PrizeOrder { get; set; }
        public string PrizeId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ManagerId { get; set; }
    }
}
