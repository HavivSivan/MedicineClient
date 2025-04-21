using Microsoft.Maui.ApplicationModel.Communication;
using MedicineClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Text.Json.Serialization;




namespace MedicineClient.Services
{
    public class MedicineWebApi
    {
        public MedicineWebApi()
        {
            client=new HttpClient();
            jsonSerializerOptions=new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive=false
            };
            LoggedInUser=new AppUser();
        }
  
        private HttpClient client;

        private JsonSerializerOptions jsonSerializerOptions;
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://zh7xvw6t-5155.euw.devtunnels.ms/api/" : "http://localhost:5155/api/";

        private string baseUrl = "https://zh7xvw6t-5155.euw.devtunnels.ms/api/";
        public AppUser LoggedInUser { get; set; }
        public async Task<AppUser?> LoginAsync(LoginInfo userInfo)
        {
            string url = $"{this.baseUrl}login";
            try
            {
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive=true
                    };
                    AppUser? result = JsonSerializer.Deserialize<AppUser>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            string url = $"{this.baseUrl}getuserbyusername";
            var response = await client.GetAsync($"{url}?username={username}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AppUser>();
            }
            Console.WriteLine("failed");
            return null;
        }

        public async Task<bool> EnableUserAsync(int userId)
        {
            string url = $"{baseUrl}enableuser?id={userId}"; 

            var response = await client.PostAsync(url, null); 

            return response.IsSuccessStatusCode;
        }

        public async Task<AppUser?> Register(AppUser user)
        {
            string url = $"{this.baseUrl}register";
            try
            {
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive=false

                    };
                    
                    AppUser? result = JsonSerializer.Deserialize<AppUser>(resContent, options);
                    Console.WriteLine($"Response Content: {resContent}");
                    return result;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Server returned error: {response.StatusCode}, {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during registration: {ex.Message}");
                return null;
            }
        }

    
    public async Task<List<Medicine>> GetMedicinesAsync()
        {
            try
            {
                Console.WriteLine("Fetching medicines for the pharmacy...");
                var response = await client.GetAsync($"{baseUrl}/getmedicines");
                if (response.IsSuccessStatusCode)
                {
                    var medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();
                    Console.WriteLine($"Fetched {medicines?.Count} medicines.");
                    return medicines ?? new List<Medicine>();
                }
                Console.WriteLine($"Failed to fetch medicines. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching medicines: {ex.Message}");
            }

            return new List<Medicine>();
        }
        public async Task<bool> UpdateMedicineAsync(Medicine medicine)
        {
            try
            {
                Console.WriteLine($"Updating medicine ID {medicine.MedicineId} to status {medicine.Status.Mstatus}...");
                var response = await client.PutAsJsonAsync($"api/medicine/{medicine.MedicineId}", medicine);

                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {await response.Content.ReadAsStringAsync()}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Medicine updated successfully.");
                    return true;
                }
                Console.WriteLine($"Failed to update medicine. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating medicine: {ex.Message}");
            }

            return false;
        }
        public async Task<List<Medicine>> GetMedicineList()
        {
            string url = $"{this.baseUrl}getmedicines";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string Content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions opt = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Medicine> listing = JsonSerializer.Deserialize<List<Medicine>>(Content, opt);
                    return listing;


                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
             public async Task<List<Order>> GetUserOrdersAsync()
        {
            try
            {
                var response = await client.GetAsync($"{baseUrl}/Order");
                if (response.IsSuccessStatusCode)
                {
                    var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                    return orders ?? new List<Order>();
                }
            }
            catch (Exception ex)
            {
            }

            return new List<Order>();
        }
        public async Task<List<Medicine>> GetUserMedicinesAsync()
        {
            try
            {
                var response = await client.GetAsync($"{baseUrl}/Medicine");
                if (response.IsSuccessStatusCode)
                {
                    var medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();
                    return medicines ?? new List<Medicine>();
                }
            }
            catch (Exception ex)
            {
            }

            return new List<Medicine>();
        }
        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/is-username-taken/{username}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return bool.Parse(content);
            }
            return true; 
        }
        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{baseUrl}/update-user", content);
            return response.IsSuccessStatusCode;
        }

    }
}

