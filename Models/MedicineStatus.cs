using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace MedicineClient.Models
{
    public class MedicineStatus
    {
        public int StatusId { get; set; }
        public string Mstatus { get; set; }
        public string Notes { get; set; }
        public MedicineStatus() { }
        public MedicineStatus(int StatusId, string Mstatus, string Notes)
        {
            this.StatusId=StatusId;
            this.Mstatus= Mstatus;
            this.Notes=Notes;
        }
    }
}