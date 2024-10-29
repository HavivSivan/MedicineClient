using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineServer.DTO
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public int Rank { get; set; }

        public AppUser() { }
       
    }
}