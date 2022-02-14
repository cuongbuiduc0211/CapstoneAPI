﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class CEByBrand
    {
        public ContestEventType Type { get; set; }
        public string BrandId { get; set; }
        public DateTime Now { get; set; } 
    }
}
