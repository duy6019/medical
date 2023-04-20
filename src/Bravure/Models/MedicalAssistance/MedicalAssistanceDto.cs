using System;
using System.Collections.Generic;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Models.MedicalAssistances
{
    public class MedicalAssistanceDto
    {
        public Guid Id { get; set; }
        public string DisplayId { get; set; }
        public Guid DepartmentId { get; set; }
        public MedicalType Type { get; set; }
        public MedicalServiceStatus Status { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
