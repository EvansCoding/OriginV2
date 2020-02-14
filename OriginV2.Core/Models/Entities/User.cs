using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;
using System.Collections.Generic;

namespace OriginV2.Core.Models.Entities
{
    public class User : IBaseEntity
    {
        public User()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PathImage { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual IList<Product> Products { get; set; }
        public virtual Account Account { get; set; }
    }
}
