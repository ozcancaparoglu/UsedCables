using Newtonsoft.Json;

namespace UsedCables.Infrastructure.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data, int expireTime)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(expireTime));
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            //TODO : DRY Principle find solution
            if (Any(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                value = JsonConvert.DeserializeObject<T>(jsonData);
                return true;
            }
            value = default;
            return false;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}