namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int totalItems, int totalPages, int itemsPerPage)
        {
            CurrentPage = currentPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
        }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
    }
}