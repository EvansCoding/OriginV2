namespace OriginV2.Web.Common
{
    using System;
    [Serializable]
    public class UserLogin
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
    }
}