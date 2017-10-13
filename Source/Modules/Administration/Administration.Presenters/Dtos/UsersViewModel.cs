using Administration.Entities;
using System.Collections.Generic;

namespace Administration.Presenters.Dtos
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
