using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Utilities.Cache
{
    public class CacheUtilities
    {
        public static ICacheManager CacheManagerGlobal
        {
            get
            {
                return SystemManager.GetCacheManager(CacheManagerInstance.Global);
            }
        }

        public static string BuildCacheKey(string cacheKey, string itemKey)
        {
            return string.Concat(cacheKey, itemKey.ToUpperInvariant());
        }
    }
}