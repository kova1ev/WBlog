using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBlog.Application.Core.Dto
{
    public class LoginModel : BaseRequestDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="email not correct")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
