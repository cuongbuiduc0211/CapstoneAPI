using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ExchangeAccessorryDetail
    {
        public string Id { get; set; }
        public string ExchangeId { get; set; }
        public string BrandId { get; set; }
        public string AccessoryName { get; set; }
        public bool IsUsed { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public int Amount { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Exchange Exchange { get; set; }
    }
}
