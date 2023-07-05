using System.ComponentModel.DataAnnotations.Schema;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new();
        // [NotMapped]
        // public int Age
        // {
        //     get
        //     {
        //         var today = DateOnly.FromDateTime(DateTime.UtcNow);
        //         var age = today.Year - DateOfBirth.Year;

        //         if (DateOfBirth > today.AddYears(-age)) age--;

        //         return age;
        //     }
        // }
    }
}