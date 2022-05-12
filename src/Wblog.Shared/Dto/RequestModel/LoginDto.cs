using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBlog.Shared.Dto
{
    public class LoginDto : BaseRequestDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="email not correct")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
