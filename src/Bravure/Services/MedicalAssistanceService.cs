using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
namespace Bravure.Services;

public class MedicalAssistanceService : IMedicalAssistanceService
{
    private readonly BravureDbContext _context;

    public MedicalAssistanceService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreateMedicalAssistance(MedicalAssistance medicalAssistance)
    {
        _context.MedicalAssistances.Add(medicalAssistance);
        _context.SaveChanges();
    }

    public MedicalAssistance GetMedicalAssistance(Guid id)
    {
        return _context.MedicalAssistances.Find(id);
    }

    public List<MedicalAssistance> GetAllMedicalAssistances()
    {
        return _context.MedicalAssistances.ToList();
    }

    public void UpdateMedicalAssistance(MedicalAssistance medicalAssistance)
    {
        // Validation logic, if any
        _context.MedicalAssistances.Update(medicalAssistance);
        _context.SaveChanges();
    }

    public void DeleteMedicalAssistance(Guid id)
    {
        var medicalAssistance = _context.MedicalAssistances.Find(id);
        if (medicalAssistance != null)
        {
            _context.MedicalAssistances.Remove(medicalAssistance);
            _context.SaveChanges();
        }
    }
}
