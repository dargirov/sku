using System;

namespace Infrastructure.Services.Multitenancy
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}
