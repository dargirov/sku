using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Store.Presenters.Dtos
{
    public class PrivilegesViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }

        public IEnumerable<Entities.Store> Stores { get; set; }

        public IEnumerable<Entities.StorePrivilege> StorePrivileges { get; set; }
    }
}
