using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedicineClient.Models
{
    public class AppUser
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPassword { get; set; }

        public int Rank { get; set; }

        public int Id { get; set; }
        public AppUser() { }
        public AppUser(int id, string username, string firstname, string lastname, string useremail, string userpassword, int rank) 
        {
            this.Id = id; this.UserName = username; this.FirstName = firstname; this.LastName=lastname; this.Email = useremail; this.UserPassword = userpassword; this.Rank = rank;
        }
       
    }
}