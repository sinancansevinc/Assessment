using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponse(string cacheKey, object response, TimeSpan timeLive);
        Task<string> GetCachedResponse(string cacheKey);
    }
}
