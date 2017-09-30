using System;

namespace Infrastructure.Services.Multitenancy
{
    public class TenantProvider : ITenantProvider
    {
        public Guid GetTenantId()
        {
            return Guid.Parse("069b57ab-6ec7-479c-b6d4-a61ba3001c86");
        }
    }
}
