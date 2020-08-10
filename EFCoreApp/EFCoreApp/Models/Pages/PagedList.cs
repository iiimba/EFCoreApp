using System.Collections.Generic;
using System.Linq;

namespace EFCoreApp.Models.Pages
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => TotalPages > CurrentPage;

        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            this.CurrentPage = options.CurrentPage;
            this.PageSize = options.PageSize;
            this.TotalPages = query.Count() / this.PageSize;
            base.AddRange(query.Skip((this.CurrentPage - 1) * this.PageSize).Take(this.PageSize));
        }
    }
}
