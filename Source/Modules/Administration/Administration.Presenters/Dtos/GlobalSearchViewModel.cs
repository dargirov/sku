using Administration.Bll.Dtos;
using System.Collections.Generic;

namespace Administration.Presenters.Dtos
{
    public class GlobalSearchViewModel
    {
        public int Count { get; set; }
        public IEnumerable<GlobalSearchResultDto> Results { get; set; }
    }
}
