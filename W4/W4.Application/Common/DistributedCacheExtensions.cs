using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Common
{
    public static class DistributedCacheExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true // Không phân biệt hoa/thường khi giải mã JSON
        };

        // 1. Hàm lưu dữ liệu bất kỳ vào Redis dưới dạng JSON kèm TTL
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null,
            CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? CacheKeys.DefaultAbsoluteExpiration,
                SlidingExpiration = unusedExpireTime ?? CacheKeys.DefaultSlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data, JsonOptions);
            await cache.SetStringAsync(recordId, jsonData, options, cancellationToken);
        }

        // 2. Hàm đọc dữ liệu từ Redis và tự động parse về kiểu T
        public static async Task<T?> GetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId,
            CancellationToken cancellationToken = default)
        {
            var jsonData = await cache.GetStringAsync(recordId, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData, JsonOptions);
        }

        // 3. Hàm xóa key khỏi Redis
        public static async Task RemoveRecordAsync(
            this IDistributedCache cache,
            string recordId,
            CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(recordId, cancellationToken);
        }
    }
}