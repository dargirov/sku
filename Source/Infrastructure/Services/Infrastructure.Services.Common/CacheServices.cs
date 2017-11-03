using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Services.Common
{
    public class CacheServices : ICacheServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CacheServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Set<T>(string key, T value)
        {
            _session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public T Get<T>(string key)
        {
            var value = _session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public void Remove(string key)
        {
            _session.Remove(key);
        }
    }
}
