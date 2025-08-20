using System.Collections.Concurrent;

namespace APITestAutomation.Helpers
{
    public static class TokenCache
    {
        private static readonly ConcurrentDictionary<string, string> _cache = new();

        public static string Get(string key)=>_cache.TryGetValue(key, out var value) ? value : string.Empty;
        public static void Set(string key, string token)=> _cache[key] = token;
        public static string BuildPPSProformaKey(string tenant, string userId) 
            => $"PPSProformaToken:{tenant}:{userId}";
        public static string BuildROPCTokenKey(string tenant, string userId)
            => $"ROPCToken:{tenant}:{userId}";
        public static void Clear()=> _cache.Clear();

    }

}
