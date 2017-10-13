using System.Collections.Generic;

namespace Store.Presenters.Dtos
{
    public class PrivilegeRequestModel
    {
        public int UserId { get; set; }

        public IEnumerable<Entities.StorePrivilege> StorePrivileges { get; set; }
    }
}
