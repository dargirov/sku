using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Presenters.Dtos
{
    public class ModulePrivilegesViewModel : IMapFrom<Entities.ModulePrivilege>
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int UserId { get; set; }

        public bool StoreRead { get; set; }
        public bool StoreWrite { get; set; }
        public bool StoreDelete { get; set; }

        public bool ManufacturerRead { get; set; }
        public bool ManufacturerWrite { get; set; }
        public bool ManufacturerDelete { get; set; }

        public bool SupplierRead { get; set; }
        public bool SupplierWrite { get; set; }
        public bool SupplierDelete { get; set; }

        public bool ProductRead { get; set; }
        public bool ProductWrite { get; set; }
        public bool ProductDelete { get; set; }
        public bool ProductImport { get; set; }

        public bool CategoryRead { get; set; }
        public bool CategoryWrite { get; set; }
        public bool CategoryDelete { get; set; }

        public bool ClientRead { get; set; }
        public bool ClientWrite { get; set; }
        public bool ClientDelete { get; set; }

        public bool IncomeRead { get; set; }
        public bool IncomeWrite { get; set; }
        public bool IncomeDelete { get; set; }

        public bool SaleRead { get; set; }
        public bool SaleWrite { get; set; }
        public bool SaleDelete { get; set; }

        public bool InvoiceRead { get; set; }
        public bool InvoiceWrite { get; set; }
        public bool InvoiceDelete { get; set; }

        public bool RequestRead { get; set; }
        public bool RequestWrite { get; set; }
        public bool RequestDelete { get; set; }
    }
}
