using System;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class Department : IdentifiableEntity<Guid>
    {
        public string DisplayId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
