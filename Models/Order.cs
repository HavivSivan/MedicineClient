using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Medicine Medicine { get; set; }
        public AppUser User { get; set; }
        public string Receiver { get; set; }

        public string Sender { get; set; }

        public Order() { }
        public Order(int id, Medicine medicine, AppUser user, string reciever, string sender)
        {
            this.Sender = sender;
            this.Medicine = medicine;
            this.User = user;
            this.Id = id;
            this.Receiver = reciever;
            
        }
    }
}