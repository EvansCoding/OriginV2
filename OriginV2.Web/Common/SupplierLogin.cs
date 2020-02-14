namespace OriginV2.Web.Common
{
    using System;
    [Serializable]
    public class SupplierLogin
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
    }
}