using AtonWebApi.DTO;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace AtonWebApi.Models
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }

        public User(string login, string password, string name, int gender, bool isAdmin, string createdBy, DateTime? birhday = null) 
        {
            Guid = Guid.NewGuid();
            Login = login;
            Password = password;
            Name = name;
            Gender = gender;
            Birthday = birhday;
            IsAdmin = isAdmin;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            ModifiedOn = DateTime.Now;
            ModifiedBy = createdBy;
        }

        public User(UserCreateDTO userDTO, string createdBy)
        {
            Guid = Guid.NewGuid();
            Login = userDTO.Login;
            Password = userDTO.Password;
            Name = userDTO.Name;
            Gender = userDTO.Gender;
            Birthday = userDTO.Birhday;
            IsAdmin = userDTO.IsAdmin;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            ModifiedOn = DateTime.Now;
            ModifiedBy = createdBy;
        }
    }
}
