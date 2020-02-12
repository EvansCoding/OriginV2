using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;
using System.Collections.Generic;

namespace OriginV2.Core.Models.Entities
{
    public class Product : IBaseEntity
    {
        public Product()
        {
            Id = GuidComb.GenerateComb();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string Origin { get; set; }
        public string Attribute { get; set; }
        public string QRHashCode { get; set; }
        public string Technology { get; set; }
        public bool Publish { get; set; }
        public DateTime HarvestAt { get; set; }
        public bool CreateAt { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual User User { get; set; }

        public virtual Product Children { get; set; }
        public virtual Product Parent { get; set; }
    }
}
