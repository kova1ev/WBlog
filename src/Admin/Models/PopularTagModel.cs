namespace WBlog.Admin.Models;

public class PopularTagModel 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int PostCount { get; set; }

}
