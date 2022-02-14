using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ExchangeCarDetail
    {
        public string Id { get; set; }
        public string ExchangeId { get; set; }
        public string BrandId { get; set; }
        public string CarName { get; set; }
        public int YearOfManufactor { get; set; }
        public string Origin { get; set; }
        public string LicensePlate { get; set; }
        public bool IsUsed { get; set; }
        public double Kilometers { get; set; }
        public int YearOfUsed { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public int Amount { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Exchange Exchange { get; set; }
    }
}
