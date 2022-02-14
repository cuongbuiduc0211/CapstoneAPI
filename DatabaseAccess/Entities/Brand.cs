using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            Accessories = new HashSet<Accessory>();
            CarModels = new HashSet<CarModel>();
            ContestEvents = new HashSet<ContestEvent>();
            ExchangeAccessorryDetails = new HashSet<ExchangeAccessorryDetail>();
            ExchangeCarDetails = new HashSet<ExchangeCarDetail>();
            InterestedBrands = new HashSet<InterestedBrand>();
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }
        public virtual ICollection<CarModel> CarModels { get; set; }
        public virtual ICollection<ContestEvent> ContestEvents { get; set; }
        public virtual ICollection<ExchangeAccessorryDetail> ExchangeAccessorryDetails { get; set; }
        public virtual ICollection<ExchangeCarDetail> ExchangeCarDetails { get; set; }
        public virtual ICollection<InterestedBrand> InterestedBrands { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
