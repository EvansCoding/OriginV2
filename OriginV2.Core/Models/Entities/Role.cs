using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;
using System.Collections.Generic;

namespace OriginV2.Core.Models.Entities
{
    public class Role : IBaseEntity
    {
        public Role()
        {
            Id = GuidComb.GenerateComb();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public IList<User> Users { get; set; }
        public IList<Supplier> Suppliers { get; set; }
    }
}
