using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace HotelFrontend.Helpers
{
    public class HttpRequest
    {

        private static readonly HttpClient client = new()
        { BaseAddress = new Uri("http://localhost:5008") };

        public static async Task<T> GetAt<T>(string Endpoint)
        {
            // fetch rooms
            using HttpResponseMessage response = await client.GetAsync(Endpoint);
            var json = await response.Content.ReadAsStringAsync();
            T data = JsonSerializer.Deserialize<T>(json);

            return data;
        }

        public static async Task<Boolean> PostAt<T>(String Endpoint, T data)
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, data);

            var str = JsonSerializer.Serialize<T>(data);
            Debug.WriteLine(str);

            response.EnsureSuccessStatusCode();

            var item = await response.Content.ReadFromJsonAsync<T>();
            Debug.WriteLine(item);

            if (response.StatusCode == HttpStatusCode.Created) return true;
            return false;
        }
    }
}
