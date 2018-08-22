using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Store.Presenters.Dtos
{
    public class ConfigViewModel : IMapFrom<Entities.Store>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Color { get; set; }
    }
}
