using System;
using System.Collections.Generic;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Models.Examinations
{
    public class MedicalExaminationDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public int Order { get; set; }
        public string DisplayID { get; set; }
        public string PatientNote { get; set; }
        public string Creator { get; set; }
        public string Note { get; set; }
        public string Conclusions { get; set; }
        public DateTime FollowUpDate { get; set; }
    }
}
