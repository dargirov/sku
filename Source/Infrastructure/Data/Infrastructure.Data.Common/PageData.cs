using System;

namespace Infrastructure.Data.Common
{
    public class PageData
    {
        public PageData(int totalResults, int page, int pageSize)
        {
            TotalResults = totalResults;
            Page = page;
            PageSize = pageSize;
        }

        public int TotalResults { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalResults / PageSize);

        public int ResultsFrom => (Page - 1) * PageSize + 1;

        public int ResultsTo => (Page * PageSize) <= TotalResults ? Page * PageSize : TotalResults;
    }
}
