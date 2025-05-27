using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineClient.Models
{
    public class LoginInfo
    {
        public string password { get; set; }
        public string username { get; set; }
    }
    public class PharmacyCreateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
    }
    public class MedicineCreateDTO
    {
        public string MedicineName { get; set; }
        public string BrandName { get; set; }
        public int PharmacyId { get; set; }
        public int UserId { get; set; }
    }
}
