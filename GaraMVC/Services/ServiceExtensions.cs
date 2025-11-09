using System.Text.Json;
using GaraMVC.Models;
namespace GaraMVC.Services
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Options cho JsonSerializer với PropertyNameCaseInsensitive
        /// </summary>
        public static JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        /// <summary>
        /// Xử lý response và deserialize JSON
        /// </summary>
        public static async Task<T?> DeserializeResponse<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return default;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, GetJsonOptions());
        }

        /// <summary>
        /// Kiểm tra response có thành công không
        /// </summary>
        public static bool IsSuccessful(this HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode;
        }
    }
}
