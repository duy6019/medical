using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Models.Examinations;

namespace Bravure.Services
{
    public interface IMedicalExaminationService
    {
        void CreateMedicalExamination(MedicalExaminationDto medicalExamination);
        void DeleteMedicalExamination(Guid id);
        List<MedicalExamination> GetAllMedicalExaminations();
        MedicalExamination GetMedicalExamination(Guid id);
        void UpdateMedicalExamination(MedicalExaminationDto medicalExamination);
    }
}