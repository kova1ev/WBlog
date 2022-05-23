using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBlog.Shared.Models
{
    public class LoginModel : BaseModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
