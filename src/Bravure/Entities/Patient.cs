using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class Patient : IdentifiableEntity<Guid>
    {
        public string DisplayID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string InsuranceNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CitizenIdentification { get; set; }

        public virtual ICollection<MedicalExamination> Examinations { get; set; }
    }
}
