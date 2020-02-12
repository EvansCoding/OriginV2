using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Models.Entities
{
    public class ProductView : IBaseEntity
    {
        public ProductView()
        {
            Id = GuidComb.GenerateComb();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string Origin { get; set; }
        public string Attribute { get; set; }
        public string Technology { get; set; }
        public string QRHashCode { get; set; }
        public DateTime HarvestAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual Supplier Supplier { get; set; }  
    }
}
