using Administration.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration.Presenters.Dtos
{
    public class ConfigApiRequestModel
    {
        public IEnumerable<ConfigOptionDto> ConfigOptions { get; set; } 
    }
}
