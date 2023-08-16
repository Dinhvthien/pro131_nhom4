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
           var resut = await httpClient.PostAsync(apiUrl, stringContent);
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
           var result =  await httpClient.PutAsync(apiUrl, stringContent);
            return true;
        }
        public async Task<bool> DeleteAll<T>(string apiUrl)
        {
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<T> GetById_DungBM<T>(string url, Guid id)
        {

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url + id);
            string TResponse = await response.Content.ReadAsStringAsync();
            T model = JsonConvert.DeserializeObject<T>(TResponse);
            return model;
        }
        public async Task<T> GetById_DungBM_2id<T>(string url, Guid id)
        {

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url + id);
            string TResponse = await response.Content.ReadAsStringAsync();
            T model = JsonConvert.DeserializeObject<T>(TResponse);
            return model;
        }

        public async Task<T> Update_DungBM<T>(string url, T model, Guid id)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url + id.ToString(), content);
            string result = await response.Content.ReadAsStringAsync();
            return model;
        }
        public async Task<int> Delete_DungBM<T>(string urlGetById, string urlRemove, Guid id)
        {
            T model = await GetById_DungBM<T>(urlGetById, id);
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.GetAsync(urlRemove + id);
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            return 1;
        }
        

        public async Task<int> Delete_DungBM_2id(string urlRemove, Guid id1, Guid id2)
        {
            HttpClient client = new HttpClient();
            //var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            //var response = await client.GetAsync(urlRemove + id1 + id2);
            var s = ($"{urlRemove}/{id1},{id2}");
            var response = await client.GetAsync(s); ;
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            return 1;
        }
    }
}
