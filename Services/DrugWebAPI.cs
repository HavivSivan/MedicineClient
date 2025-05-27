using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicineClient.Services
{
    public class DrugWebAPIRequest
    {
        public string barcode { get; set; }
        public bool prescription { get; set; }
        public DrugWebAPIRequest(string barcode, bool prescription)
        {
            this.barcode = barcode;
            this.prescription = prescription;
        }
    }
    public class DrugWebAPI
    {
        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "israeldrugs.health.gov.il";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://israeldrugs.health.gov.il";
        #endregion

        public DrugWebAPI() 
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();
            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public async Task<string> SearchByBarcode(string code)
        {
            DrugWebAPIRequest input = new DrugWebAPIRequest(code, false);
            string url = $"{this.baseUrl}/GovServiceList/IDRServer/SearchByBarcode";
            try
            {
                string json = JsonSerializer.Serialize(input);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(resContent))
                    {
                        JsonElement root = doc.RootElement;

                        if (root.TryGetProperty("results", out JsonElement resultsArray) &&
                            resultsArray.ValueKind == JsonValueKind.Array &&
                            resultsArray.GetArrayLength() > 0)
                        {
                            JsonElement firstResult = resultsArray[0];
                            if (firstResult.TryGetProperty("dragEnName", out JsonElement nameElement))
                            {
                                return nameElement.GetString();
                            }
                        }

                        Console.WriteLine( "dragEnName not found in results.");
                    }
                }
                else
                {
                    Console.WriteLine( $"Request failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( $"Exception: {ex.Message}");
            }
            return "";
        }

    }
}
