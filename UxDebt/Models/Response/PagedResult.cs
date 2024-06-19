namespace UxDebt.Models.Response
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
    }
}
