using Microsoft.Maui.ApplicationModel.Communication;
using MedicineClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;



namespace MedicineClient.Services
{
    public class MedicineWebApi
    {
        //מנהל תכונות מתקדמות של בקשות HTTP
        //cookies כמו תמיכה
        private HttpClient client;

        // JSON משתנה זה יכיל את ההגדרות שייקבעו בהמשך כיצד לעבד ולהמיר נתוני
        // בעת שליחת וקבלת בקשות מהשרת
        private JsonSerializerOptions jsonSerializerOptions;

        // כתובת הבסיס לכתובת השרת מותאמת לפי פלטפורמות ההרצה
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://zj6frcmf-5155-inspect.euw.devtunnels.ms/" : "http://localhost:5021/api/";


        // אובייקט של מחלקת השירות שמכיל את כתובת הבסיס לשרת
        private string baseUrl;


        //מאפיין זה מחזיק את פרטי המשתמש לאחר התחברות מוצלחת.
        //ניתן להשתמש בו לצורך בדיקה או שליפה של מידע על המשתמש המחובר
        public AppUser LoggedInUser { get; set; }

    }
}

