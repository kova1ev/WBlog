using System.ComponentModel.DataAnnotations;

namespace WBlog.Shared.Models
{
    public enum DateState
    {
        [Display(Name = "Старые")]
        DateAsc = 1,
        [Display(Name = "Новые")]
        DateDesc = 0
    }
}