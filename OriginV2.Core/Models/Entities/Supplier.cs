using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;
using System.Collections.Generic;

namespace OriginV2.Core.Models.Entities
{
    public class Supplier : IBaseEntity
    {
        public Supplier()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PathImage { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual IList<Product> Products { get; set; }    
        public virtual IList<ProductView> ProductViews { get; set; }
        public virtual Account Account { get; set; }

    }
}
