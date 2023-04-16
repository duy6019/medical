using System;
using System.Collections.Generic;
using Bravure.Entities;

namespace Bravure.Services
{
    public interface IMedicalAssistanceService
    {
        void CreateMedicalAssistance(MedicalAssistance medicalAssistance);
        void DeleteMedicalAssistance(Guid id);
        List<MedicalAssistance> GetAllMedicalAssistances();
        MedicalAssistance GetMedicalAssistance(Guid id);
        void UpdateMedicalAssistance(MedicalAssistance medicalAssistance);
    }
}