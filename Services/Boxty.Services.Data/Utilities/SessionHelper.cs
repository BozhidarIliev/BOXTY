namespace Boxty.Services.Data
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task SetObjectAsJsonAsync(this ISession session, string key, object value)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static async Task<T> GetObjectFromJsonAsync<T>(this ISession session, string key)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
