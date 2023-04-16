using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class MedicalExamination : IdentifiableEntity<Guid>
    {
        public Guid PatientId { get; set; }
        public int Order { get; set; }
        public string DisplayID { get; set; }
        public string PatientNote { get; set; }
        public string Creator { get; set; }
        public string Note { get; set; }
        public string Conclusions { get; set; }
        public DateTime FollowUpDate { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual ICollection<MedicalAssistance> MedicalAssistances { get; set; }
    }
}
