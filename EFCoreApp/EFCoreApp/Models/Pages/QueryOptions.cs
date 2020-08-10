namespace EFCoreApp.Models.Pages
{
    public class QueryOptions
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public string OrderPropertyName { get; set; }

        public bool DescendingOrder { get; set; }

        public string SearchPropertyName { get; set; }

        public string SearchTerm { get; set; }
    }
}
