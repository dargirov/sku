using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Request.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Request.Presenters.Dtos
{
    public class EditViewModel : IMapFrom<Entities.Request>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public ICollection<StockRequest> StockRequests { get; set; }

        public string Text { get; set; }

        [Required]
        public int ToStoreId { get; set; }

        [Required]
        public ICollection<Store.Entities.Store> ToStores { get; set; }

        public RequestStatusEnum Status { get; set; }

        public IEnumerable<SelectListItem> Priorities { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entities.Request, EditViewModel>()
                .ForMember(x => x.ToStoreId, opt => opt.MapFrom(x => x.StockRequests.First().ToStoreId));
        }
    }
}
