using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bravure.Services;

public class CilinicExaminationService : ICilinicExaminationService
{
    private readonly BravureDbContext _context;

    public CilinicExaminationService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreateCilinicExamination(CilinicExamination medicalExamination)
    {
        _context.CilinicExaminations.Add(medicalExamination);
        _context.SaveChanges();
    }

    public CilinicExamination GetCilinicExamination(Guid id)
    {
        return _context.CilinicExaminations.Find(id);
    }

    public List<CilinicExamination> GetAllCilinicExaminations()
    {
        return _context.CilinicExaminations
            .Include(x => x.MedicalAssistances)
            .ToList();
    }

    public void UpdateCilinicExamination(CilinicExamination medicalExamination)
    {
        // Validation logic, if any
        _context.CilinicExaminations.Update(medicalExamination);
        _context.SaveChanges();
    }

    public void DeleteCilinicExamination(Guid id)
    {
        var medicalExamination = _context.CilinicExaminations.Find(id);
        if (medicalExamination != null)
        {
            _context.CilinicExaminations.Remove(medicalExamination);
            _context.SaveChanges();
        }
    }
}
