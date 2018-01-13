using Administration.Bll.Dtos;
using System.Collections.Generic;

namespace Administration.Presenters.Dtos
{
    public class ConfigApiViewModel
    {
        public IEnumerable<ConfigOptionDto> ConfigOptions { get; set; }
        public string OrganizationHash { get; set; }
    }
}
