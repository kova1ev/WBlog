namespace WBlog.Application.Core
{
    public abstract class PageOptions
    {
        private const int maxLimit = 50;
        private int _limit = 10;
        public int Limit
        {
            get => _limit;
            set
            {
                _limit = value > maxLimit ? _limit : value;
            }
        }

        public int OffSet { get; set; } = 0;

    }
}