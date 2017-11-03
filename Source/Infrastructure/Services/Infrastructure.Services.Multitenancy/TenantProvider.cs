using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Services.Multitenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetTenantId()
        {
            var tenantId = _httpContextAccessor?.HttpContext?.Session?.GetString("tenant_id");
            if (tenantId == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(JsonConvert.DeserializeObject<string>(tenantId));
        }
    }
}
