using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.Infrastructure.Utils.Pagination
{
    public class Paginator<T> : IPaginator<T>
    {
        private IEnumerable<T> _query;
        private readonly int _onOnePage;
        private readonly bool _includePagesCount;
        private int _pagesCount;
        private int _currentPage;

        public Paginator(IEnumerable<T> query, int onOnePage, bool includePagesCount = true)
        {
            _query = query;
            _onOnePage = onOnePage;
            _includePagesCount = includePagesCount;
        }

        public IEnumerable<T> this[int page] => _query.Skip((page - 1) * _onOnePage).Take(_onOnePage);

        public int PagesCount
        {
            get
            {
                if (_includePagesCount)
                {
                    if (_pagesCount == 0)
                        _pagesCount = (int)Math.Ceiling(_query.Count() / (double)_onOnePage);

                    return _pagesCount;
                }

                return 1; //Default one page to show.
            }
        }

        public IEnumerable<T> CurrentPageValues => this[_currentPage];

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if(value <= 0)
                    throw new ArgumentException("Invalid current page value");
                _currentPage = value;
            }
        }
    }
}
