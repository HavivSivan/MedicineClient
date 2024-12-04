using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
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
        public AppUser(int id, string username, string firstname, string lastname, string useremail, string userpassword, int rank) 
        {
            this.Id = id; this.UserName = username; this.FirstName = firstname; this.LastName=lastname; this.UserEmail = useremail; this.UserPassword = userpassword; this.Rank = rank;
        }
       
    }
}