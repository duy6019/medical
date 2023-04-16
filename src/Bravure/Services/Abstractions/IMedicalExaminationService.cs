using System;
using System.Collections.Generic;
using Bravure.Entities;

namespace Bravure.Services
{
    public interface IMedicalExaminationService
    {
        void CreateMedicalExamination(MedicalExamination medicalExamination);
        void DeleteMedicalExamination(Guid id);
        List<MedicalExamination> GetAllMedicalExaminations();
        MedicalExamination GetMedicalExamination(Guid id);
        void UpdateMedicalExamination(MedicalExamination medicalExamination);
    }
}