using System;
using System.Collections.Generic;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Models.Patients
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string DisplayID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string InsuranceNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CitizenIdentification { get; set; }
    }
}
