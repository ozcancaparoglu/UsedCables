using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace UsedCables.Infrastructure.Cache.Redis
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly RedisConfigurationOptions _configuration;
        private readonly IDatabase _database;
        private readonly int _currentDatabaseId = 0;

        public RedisServer(IOptions<RedisConfigurationOptions> options)
        {
            _configuration = options.Value;

            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_configuration.Host}:{_configuration.Port},{_configuration.Admin}");
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer($"{_configuration.Host}:{_configuration.Port}").FlushDatabase(_currentDatabaseId);
        }
    }
}