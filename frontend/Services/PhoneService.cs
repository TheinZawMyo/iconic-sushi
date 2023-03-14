using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using frontend.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace frontend.Services
{
    public class PhoneService
    {

        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        public PhoneService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Phone>> GetAllPhones()
        {
            var response = await this.client.GetAsync("/api/inventory");

            var json = await response.Content.ReadAsStringAsync();
            var myData = JsonSerializer.Deserialize<IEnumerable<Phone>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return myData;
        }

        //     public async Task<MyDataModel> GetMyDataByIdAsync(int id)
        //     {
        //         var response = await _httpClient.GetAsync($"{_apiBaseUrl}/mydata/{id}");
        //         response.EnsureSuccessStatusCode();

        //         var json = await response.Content.ReadAsStringAsync();
        //         var myData = JsonSerializer.Deserialize<MyDataModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //         return myData;
        //     }

        // public async Task AddPhone(Phone phone)
        // {
        //     var json = JsonSerializer.Serialize(phone);
        //     var data = new StringContent(json, Encoding.UTF8, "application/json");

        //     var response = await this.client.PostAsync("api/inventory", data);
        //     response.EnsureSuccessStatusCode();
        // }

        //     public async Task UpdateMyDataAsync(MyDataModel myData)
        //     {
        //         var json = JsonSerializer.Serialize(myData);
        //         var data = new StringContent(json, Encoding.UTF8, "application/json");

        //         var response = await _httpClient.PutAsync($"{_apiBaseUrl}/mydata/{myData.Id}", data);
        //         response.EnsureSuccessStatusCode();
        //     }

        //     public async Task DeleteMyDataAsync(int id)
        //     {
        //         var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/mydata/{id}");
        //         response.EnsureSuccessStatusCode();
        //     }
    }

}