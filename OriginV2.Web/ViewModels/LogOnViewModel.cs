namespace OriginV2.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}