using Bravure.Entities;
using Bravure.Entities.Enums;

namespace Bravure.Models.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string DisplayId { get; set; }
        public string FullName { get; set; }
        public string PositionTitle { get; set; }
        public string Position { get; set; }
        public string IdentityNumber { get; set; }
        public string DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }

        public Department Department { get; set; }
    }
}
