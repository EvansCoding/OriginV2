using System;

namespace OriginV2.Web.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string Origin { get; set; }
        public string Attribute { get; set; }
        public string Technology { get; set; }
        public string NameSupplier { get; set; }
        public string AddressSupplier { get; set; }
        public string HashCode { get; set; }
        public DateTime Harvest { get; set; }
    }
}