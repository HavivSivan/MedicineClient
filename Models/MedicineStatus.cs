using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace MedicineClient.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public string Receiver { get; set; }

        public string Sender { get; set; }

        public Order() { }
        public Order(int id, int medicineId, int userId, string receiver, string sender)
        {
            this.Id = id;
            this.MedicineId = medicineId;
            this.UserId = userId;
            this.Receiver = receiver;
            this.Sender = sender;
        }
    }
}