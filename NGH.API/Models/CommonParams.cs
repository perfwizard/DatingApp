namespace NGH.API.Models
{
    public class CommonParams
    {
        private const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;
        
        private int pageSize = 10;
        public int PageSize 
        { 
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public string SearchString { get; set; }

    }
}