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
        
        public async Task<ExpandoObject> SearchByName(ExpandoObject input)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}/GovServiceList/IDRServer/SearchByName";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(input);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ExpandoObject? result = JsonSerializer.Deserialize<ExpandoObject>(resContent, options);
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

    }
}
