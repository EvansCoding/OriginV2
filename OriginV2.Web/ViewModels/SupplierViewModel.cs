using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OriginV2.Web.ViewModels
{
    public class SupplierViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PathImage { get; set; }
    }
}