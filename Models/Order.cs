using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OStatus{ get; set; }
        public Medicine Medicine { get; set; }
        public AppUser User { get; set; }
        public string? PrescriptionImage { get; set; } 

        public Order() { }
        public Order(int id, Medicine medicine, AppUser user, string? PrescriptionImage)
        {
            this.Medicine = medicine;
            this.User = user;
            this.Id = id;
            this.PrescriptionImage=PrescriptionImage;
        }
    }
}