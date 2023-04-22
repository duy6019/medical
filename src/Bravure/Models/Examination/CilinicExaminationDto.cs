using System;
using System.Collections.Generic;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Models.Examinations
{
    public class CilinicExaminationDto
    {
        public Guid Id { get; set; }
        public string DisplayID { get; set; }
        public Guid PatientId { get; set; }
        public DateTime MedicalDate { get; set; }
        public int Order { get; set; }
        public string Creator { get; set; }
        public string DoctorExc { get; set; }
        public TimeSpan MedicalTime { get; set; }
        public string Conclusions { get; set; }
    }
}
