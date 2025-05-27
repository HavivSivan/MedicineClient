using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace MedicineClient.Models
{
    public class Pharmacy
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string Phone { get; set; }
        public AppUser User { get; set; }

        public Pharmacy() { }
        public Pharmacy(int Id, string name, string adress, string phone, AppUser user)
        {
            this.Id= Id; this.Name= name; this.Address = adress; this.Phone = phone;
            User=user;
        }
        public Pharmacy( string name, string adress, string phone, AppUser user)
        { 
            this.Name= name; this.Address = adress; this.Phone = phone;
            User=user;
        }
    }
}