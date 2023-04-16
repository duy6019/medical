using System;
using System.Collections.Generic;
using Bravure.Entities;

namespace Bravure.Services
{
    public interface ICilinicExaminationService
    {
        void CreateCilinicExamination(CilinicExamination medicalExamination);
        void DeleteCilinicExamination(Guid id);
        List<CilinicExamination> GetAllCilinicExaminations();
        CilinicExamination GetCilinicExamination(Guid id);
        void UpdateCilinicExamination(CilinicExamination medicalExamination);
    }
}