﻿using Microsoft.Maui.ApplicationModel.Communication;
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
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;




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
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://zh7xvw6t-5155.euw.devtunnels.ms/api" : "http://localhost:5155/api/";
        private static string ServerUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://zh7xvw6t-5155.euw.devtunnels.ms/" : "http://localhost:5155";

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
                    if (result != null)
                        LoggedInUser=result;
                        
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
                var response = await client.GetAsync($"{baseUrl}getmedicines");
                if (response.IsSuccessStatusCode)
                {
                    var medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();

                   
                    return medicines.Where(x=>x.Pharmacy.User.Id==LoggedInUser.Id).ToList() ?? new List<Medicine>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetMedicinesAsync failed: {ex}");
            }

            return new List<Medicine>();
        }
        public async Task<bool> UpdateMedicineAsync(Medicine medicine)
        {
            try
            {
                var json = JsonSerializer.Serialize(medicine);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}UpdateMedicine", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
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
        public async Task<List<Medicine>> GetUserMedicinesAsync()
        {
            try
            {
                var response = await client.GetAsync($"{baseUrl}getmedicines");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<Medicine>>()
                           ?? new List<Medicine>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserMedicinesAsync failed: {ex}");
            }
            return new List<Medicine>();
        }

        public async Task<List<Order>> GetUserOrdersAsync()
        {
            try
            {
                var response = await client.GetAsync($"{baseUrl}GetOrdersList");
                if (response.IsSuccessStatusCode)
                {
                    var list = await response.Content.ReadFromJsonAsync<List<Order>>();
                    list.Where(x => x.User.Id == LoggedInUser.Id).ToList();
                    return list
                           ?? new List<Order>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserOrdersAsync failed: {ex}");
            }
            return new List<Order>();
        }
        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}is-username-taken/{username}");
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
            
            var response = await client.PutAsync($"{baseUrl}update-user", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> OrderMedicine(Medicine medicine, string? PrescriptionImage)
        {
            OrderDTO order = new OrderDTO
            {
                MedicineId  = medicine.MedicineId,
                UserId = LoggedInUser.Id,
                PrescriptionImage = PrescriptionImage,
                OStatus = "Pending"
            };
            try
            {
                var json = JsonSerializer.Serialize(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{this.baseUrl}Order", content);
                if (response!=null)
                {
                    return response.IsSuccessStatusCode;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public async Task<string?> UploadPrescriptionImage(byte[] imageBytes, string fileName)
        {
            try
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(imageBytes);
                form.Add(fileContent, "file", fileName);

                var response = await client.PostAsync($"{baseUrl}UploadPrescriptionImage", form);
                if (!response.IsSuccessStatusCode)
                    return null;

                string imageUrl = await response.Content.ReadAsStringAsync();
                return $"{ServerUrl}{imageUrl}";
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateOrderStatus(Order order)
        {
            OrderDTO orderDTO= new OrderDTO
            {
                MedicineId = order.Medicine.MedicineId,
                UserId = order.User.Id,
                PrescriptionImage = order.PrescriptionImage,
                OStatus = order.OStatus
            };
            try
            {
                var json = JsonSerializer.Serialize(orderDTO);
                var url = $"{this.baseUrl}UpdateOrderStatus?Orderid={order.Id}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response!=null)
                {
                    return response.IsSuccessStatusCode;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<ObservableCollection<Order>> GetOrdersList()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUrl}GetOrdersList");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Order> orderList = JsonSerializer.Deserialize<List<Order>>(content, options);
                    if (orderList != null)
                    {

                        foreach (var order in orderList)
                        {
                            if (order.Medicine?.Pharmacy?.User?.Id == LoggedInUser.Id)
                                orders.Add(order);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }
            return orders;
        }
        public async Task<bool> AddMedicine(MedicineCreateDTO medicine)
        {
            string url = $"{this.baseUrl}AddMedicine";
            try
            {
                string json = JsonSerializer.Serialize(medicine);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddPharmacy(PharmacyCreateDTO pharmacy)
        {
            string url = $"{this.baseUrl}AddPharmacy";
            try
            {
                string json = JsonSerializer.Serialize(pharmacy);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Pharmacy> GetPharmacy()
        {
            string url = $"{this.baseUrl}GetPharmacies";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Pharmacy> pharmacies = JsonSerializer.Deserialize<List<Pharmacy>>(content, options);
                    Pharmacy pharmacy = pharmacies.FirstOrDefault(p => p.User.Id == LoggedInUser.Id);
                    return pharmacy;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching pharmacy: {ex.Message}");
                return null;
            }
        }
    }
    public class OrderDTO
    {
        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public string? PrescriptionImage { get; set; }
        public string OStatus { get; set; } = "Pending";
    }
    public class  OrderUpdateDTO
    {
        public OrderDTO OrderDTO { get; set; }
        public int OrderId { get; set; }
    }

}

