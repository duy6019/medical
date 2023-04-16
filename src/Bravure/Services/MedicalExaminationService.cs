using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
namespace Bravure.Services;

public class MedicalExaminationService : IMedicalExaminationService
{
    private readonly BravureDbContext _context;

    public MedicalExaminationService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreateMedicalExamination(MedicalExamination medicalExamination)
    {
        _context.MedicalExaminations.Add(medicalExamination);
        _context.SaveChanges();
    }

    public MedicalExamination GetMedicalExamination(Guid id)
    {
        return _context.MedicalExaminations.Find(id);
    }

    public List<MedicalExamination> GetAllMedicalExaminations()
    {
        return _context.MedicalExaminations.ToList();
    }

    public void UpdateMedicalExamination(MedicalExamination medicalExamination)
    {
        // Validation logic, if any
        _context.MedicalExaminations.Update(medicalExamination);
        _context.SaveChanges();
    }

    public void DeleteMedicalExamination(Guid id)
    {
        var medicalExamination = _context.MedicalExaminations.Find(id);
        if (medicalExamination != null)
        {
            _context.MedicalExaminations.Remove(medicalExamination);
            _context.SaveChanges();
        }
    }
}
