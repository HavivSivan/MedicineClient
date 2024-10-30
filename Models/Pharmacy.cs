using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
{
    public class Pharmacy
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;

        public int Phone { get; set; }

        public Pharmacy() { }
        public Pharmacy(int Id, string name, string adress, int phone)
        {
           this.Id= Id; this.Name= name; this.Adress = adress; this.Phone = phone;
        }
    }
}