namespace Infrastructure.Services.Common
{
    public interface ICacheServices
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
