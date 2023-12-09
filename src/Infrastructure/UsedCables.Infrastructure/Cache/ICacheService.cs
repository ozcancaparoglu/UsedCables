namespace UsedCables.Infrastructure.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);

        bool TryGetValue<T>(string key, out T value);

        void Add(string key, object data, int expireTime);

        void Remove(string key);

        void Clear();

        bool Any(string key);
    }
}