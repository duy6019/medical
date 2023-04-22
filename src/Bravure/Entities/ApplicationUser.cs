using System;
using System.Collections.Generic;
using Bravure.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bravure.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayId { get; set; }
        public string FullName { get; set; }
        public string PositionTitle { get; set; }
        public string Position { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
    }
}
