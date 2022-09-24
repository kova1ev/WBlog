namespace WBlog.WebUI;

public class SiteOptions
{
    public string? BaseAddress { get; set; }
    public string GitHubLink { get; set; } = string.Empty;
    public string TelegramLink { get; set; } = string.Empty;
    public int BarSize { get; set; } = 5;
}