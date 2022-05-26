using System.Collections.Generic;

namespace WebStore.Infrastructure.Utils.Pagination
{
    public interface IPaginator<T>
    {
        int PagesCount { get; }

        IEnumerable<T> this[int page] { get; }

        IEnumerable<T> CurrentPageValues { get; }

        int CurrentPage { get; set; }
    }
}
