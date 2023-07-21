using Newtonsoft.Json;
using System.Text;

namespace App_client.Services
{
    public class TServices
    {
        public async Task<List<T>> GetAll<T>(string apiUrl)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            List<T> list = JsonConvert.DeserializeObject<List<T>>(apiData);
            return list;
        }
        public async Task<T> GetAllById<T>(string apiUrl)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            T prop = JsonConvert.DeserializeObject<T>(apiData);
            return prop;
        }
        public async Task<bool> CreateAll<T>(string apiUrl, T values)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
                                                      // Convert registerUser to JSON
            var valuesJSON = JsonConvert.SerializeObject(values);
            // Convert to string content
            var stringContent = new StringContent(valuesJSON, Encoding.UTF8, "application/json");
            // Send request POST to register API
            await httpClient.PostAsync(apiUrl, stringContent);
            return true;
        }
        public async Task<bool> EditAll<T>(string apiUrl, T values)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
                                                      // Convert registerUser to JSON
            var valuesJSON = JsonConvert.SerializeObject(values);
            // Convert to string content
            var stringContent = new StringContent(valuesJSON, Encoding.UTF8, "application/json");
            // Send request POST to register API
            await httpClient.PutAsync(apiUrl, stringContent);
            return true;
        }
        public async Task<bool> DeleteAll<T>(string apiUrl)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            await httpClient.DeleteAsync(apiUrl);
            return true;
        }
    }
}
