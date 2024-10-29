using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineServer.DTO
{
    public class Order
    {
        public int Id { get; set; }

        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public string Receiver { get; set; }

        public string Sender { get; set; }

        public Order() { }
        
    }
}