using OriginV2.Core.Interfaces;
using OriginV2.Core.Utilities;
using System;

namespace OriginV2.Core.Models.Entities
{
    public class Account : IBaseEntity
    {
        public Account()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual User User { get; set; }
    }
}
