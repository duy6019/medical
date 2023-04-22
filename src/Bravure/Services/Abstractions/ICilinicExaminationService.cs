using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Models.Examinations;

namespace Bravure.Services
{
    public interface ICilinicExaminationService
    {
        void CreateCilinicExamination(CilinicExaminationDto medicalExamination);
        void DeleteCilinicExamination(Guid id);
        List<CilinicExamination> GetAllCilinicExaminations();
        CilinicExamination GetCilinicExamination(Guid id);
        void UpdateCilinicExamination(CilinicExaminationDto medicalExamination);
    }
}