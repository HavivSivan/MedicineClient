using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
{
    public class MedicineStatus
    {
        public int Id { get; set; }

        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public string Receiver { get; set; }

        public string Sender { get; set; }

        public MedicineStatus() { }
        public MedicineStatus(int id, int medicineid, int userid, string reciever, string sender)
        {
            this.Sender = sender;
            this.MedicineId = medicineid;
            this.UserId = userid;
            this.Id = id;
            this.Receiver = reciever;
            
        }
    }
}