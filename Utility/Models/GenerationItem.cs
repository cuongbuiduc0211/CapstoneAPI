
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class GenerationItem
    {
        public string CarModelId { get; set; }
        public string Name { get; set; }
        public int YearOfManufactor { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
    }
}
