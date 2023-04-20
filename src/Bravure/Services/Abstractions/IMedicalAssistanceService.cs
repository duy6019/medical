using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Models.MedicalAssistances;

namespace Bravure.Services
{
    public interface IMedicalAssistanceService
    {
        void CreateMedicalAssistance(MedicalAssistanceDto medicalAssistance);
        void DeleteMedicalAssistance(Guid id);
        List<MedicalAssistance> GetAllMedicalAssistances();
        MedicalAssistance GetMedicalAssistance(Guid id);
        void UpdateMedicalAssistance(MedicalAssistanceDto medicalAssistance);
    }
}
