using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace MedicineClient.Models
{
    public class Pharmacy
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;

        public string Phone { get; set; }
        public AppUser User { get; set; }

        public Pharmacy() { }
        public Pharmacy(int Id, string name, string adress, string phone, AppUser user)
        {
            this.Id= Id; this.Name= name; this.Adress = adress; this.Phone = phone;
            User=user;
        }
    }
}