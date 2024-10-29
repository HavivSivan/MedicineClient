using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineServer.DTO
{
    public class Pharmacy
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;

        public int Phone { get; set; }

        public Pharmacy() { }
        public Pharmacy()
        {
           
        }
    }
}