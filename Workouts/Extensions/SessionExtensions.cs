using Microsoft.AspNetCore.Http;

using System.Text.Json;


namespace Workouts.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value) where T : class, new()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            string serialized = JsonSerializer.Serialize<T>(value, options);
            session.SetString(key, serialized);
        }
        public static T Get<T>(this ISession session, string key) where T : class, new()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            string value = session.GetString(key);
            if (value != null)
            {
                T returnValue = JsonSerializer.Deserialize<T>(value);
                return returnValue;
            }

            return null;
        }
    }
}
