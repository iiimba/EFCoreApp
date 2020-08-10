using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

            if (options != null)
            {
                if (!string.IsNullOrWhiteSpace(options.OrderPropertyName))
                {
                    query = Order(query, options.OrderPropertyName, options.DescendingOrder);
                }

                if (!string.IsNullOrWhiteSpace(options.SearchPropertyName)
                    && !string.IsNullOrWhiteSpace(options.SearchTerm))
                {
                    query = Search(query, options.SearchPropertyName, options.SearchTerm);
                }
            }

            base.AddRange(query.Skip((this.CurrentPage - 1) * this.PageSize).Take(this.PageSize));
        }

        private IQueryable<T> Order(IQueryable<T> query, string propertyName, bool descendingOrder)
        {
            var parameter = Expression.Parameter(typeof(T), "х");
            var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), source.Type), source, parameter);
            return typeof(Queryable).GetMethods().Single(method =>
                    method.Name == (descendingOrder ? "OrderByDescending" : "OrderBy")
                    && method.IsGenericMethodDefinition
                    && method.GetGenericArguments().Length == 2
                    && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), source.Type)
                .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }

        private IQueryable<T> Search(IQueryable<T> query, string propertyName, string searchTerm)
        {
            var parameter = Expression.Parameter(typeof(T), "х");
            var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var body = Expression.Call(source, "Contains", Type.EmptyTypes, Expression.Constant(searchTerm, typeof(string)));
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return query.Where(lambda);
        }
    }
}
