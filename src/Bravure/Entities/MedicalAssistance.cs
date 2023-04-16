using System;
using System.Collections.Generic;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class MedicalAssistance : IdentifiableEntity<Guid>
    {
        public string DisplayId { get; set; }
        public Guid DepartmentId { get; set; }
        public MedicalType Type { get; set; }
        public MedicalServiceStatus Status { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<MedicalExamination> MedicalExaminations { get; set; }
        public virtual ICollection<CilinicExamination> CilinicExaminations { get; set; }
    }
}
