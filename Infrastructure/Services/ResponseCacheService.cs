using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponceAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var options = new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponce = JsonSerializer.Serialize(response,options);

            await _database.StringSetAsync(cacheKey,serializedResponce,timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cacheResponce = await _database.StringGetAsync(cacheKey);

            if (cacheResponce.IsNullOrEmpty)
            {
                return null;
            }

            return cacheResponce;
        }
    }
}