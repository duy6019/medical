using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class CilinicExamination : IdentifiableEntity<Guid>
    {
        public string DisplayID { get; set; }
        public Guid PatientId { get; set; }
        public DateTime MedicalDate { get; set; }
        public int Order { get; set; }
        public string Creator { get; set; }
        public string DoctorExc { get; set; }
        public TimeSpan MedicalTime { get; set; }
        public string Conclusions { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual ICollection<MedicalAssistance> MedicalAssistances { get; set; }
    }
}
