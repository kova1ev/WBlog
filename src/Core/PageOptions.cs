namespace WBlog.Core;

public abstract class PageOptions
{
    private const int MAXLIMIT = 100;
    private int _limit = 10;

    public int Limit
    {
        get => _limit;
        set { _limit = value > MAXLIMIT ? _limit : value; }
    }

    public int OffSet { get; set; } = 0;
}