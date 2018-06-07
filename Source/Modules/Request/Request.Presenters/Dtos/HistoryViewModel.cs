using Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Request.Presenters.Dtos
{
    public class HistoryViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public (IEnumerable<Administration.Entities.Memo> memos, PageData pageData) Memos { get; set; }
    }
}
