using System.ComponentModel.DataAnnotations;

namespace OriginV2.Web.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleID { get; set; }
        public string PathImage { get; set; }
    }
}