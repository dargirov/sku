using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Administration.Presenters.Dtos
{
    public class ModulePrivilegesRequestModel : IMapTo<Entities.ModulePrivilege>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool StoreRead { get; set; }
        [Required]
        public bool StoreWrite { get; set; }
        [Required]
        public bool StoreDelete { get; set; }

        [Required]
        public bool ManufacturerRead { get; set; }
        [Required]
        public bool ManufacturerWrite { get; set; }
        [Required]
        public bool ManufacturerDelete { get; set; }

        [Required]
        public bool SupplierRead { get; set; }
        [Required]
        public bool SupplierWrite { get; set; }
        [Required]
        public bool SupplierDelete { get; set; }

        [Required]
        public bool ProductRead { get; set; }
        [Required]
        public bool ProductWrite { get; set; }
        [Required]
        public bool ProductDelete { get; set; }
        [Required]
        public bool ProductImport { get; set; }

        [Required]
        public bool CategoryRead { get; set; }
        [Required]
        public bool CategoryWrite { get; set; }
        [Required]
        public bool CategoryDelete { get; set; }

        [Required]
        public bool ClientRead { get; set; }
        [Required]
        public bool ClientWrite { get; set; }
        [Required]
        public bool ClientDelete { get; set; }

        [Required]
        public bool IncomeRead { get; set; }
        [Required]
        public bool IncomeWrite { get; set; }
        [Required]
        public bool IncomeDelete { get; set; }

        [Required]
        public bool SaleRead { get; set; }
        [Required]
        public bool SaleWrite { get; set; }
        [Required]
        public bool SaleDelete { get; set; }

        [Required]
        public bool InvoiceRead { get; set; }
        [Required]
        public bool InvoiceWrite { get; set; }
        [Required]
        public bool InvoiceDelete { get; set; }

        [Required]
        public bool RequestRead { get; set; }
        [Required]
        public bool RequestWrite { get; set; }
        [Required]
        public bool RequestDelete { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ModulePrivilegesRequestModel, Entities.ModulePrivilege>()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
