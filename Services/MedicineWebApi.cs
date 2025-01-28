using Microsoft.Maui.ApplicationModel.Communication;
using MedicineClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;



namespace MedicineClient.Services
{
    public class MedicineWebApi
    {
        //מנהל תכונות מתקדמות של בקשות HTTP
        //cookies כמו תמיכה
        private HttpClient client = new HttpClient();

        // JSON משתנה זה יכיל את ההגדרות שייקבעו בהמשך כיצד לעבד ולהמיר נתוני
        // בעת שליחת וקבלת בקשות מהשרת
        private JsonSerializerOptions jsonSerializerOptions;

        // כתובת הבסיס לכתובת השרת מותאמת לפי פלטפורמות ההרצה
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://n3ts70gr-5155.euw.devtunnels.ms/api/" : "http://localhost:5155/api/";


        // אובייקט של מחלקת השירות שמכיל את כתובת הבסיס לשרת
        private string baseUrl = "n3ts70gr-5155.euw.devtunnels.ms/api/";


        //מאפיין זה מחזיק את פרטי המשתמש לאחר התחברות מוצלחת.
        //ניתן להשתמש בו לצורך בדיקה או שליפה של מידע על המשתמש המחובר
        public AppUser LoggedInUser { get; set; }
        public async Task<AppUser?> LoginAsync(LoginInfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
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
        public async Task<List<Medicine>> GetMedicineList()
        {
            string url = $"{this.baseUrl}GetMedicineList";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string Content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions opt = new JsonSerializerOptions
                    {

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
        public async Task<AppUser?> Register(AppUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}register";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
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
        

    }
}

