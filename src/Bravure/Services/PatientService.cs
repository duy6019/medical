using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
namespace Bravure.Services;

public class PatientService : IPatientService
{
    private readonly BravureDbContext _context;

    public PatientService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
    }

    public Patient GetPatient(Guid id)
    {
        return _context.Patients.Find(id);
    }

    public List<Patient> GetAllPatients()
    {
        return _context.Patients.ToList();
    }

    public void UpdatePatient(Patient patient)
    {
        // Validation logic, if any
        _context.Patients.Update(patient);
        _context.SaveChanges();
    }

    public void DeletePatient(Guid id)
    {
        var patient = _context.Patients.Find(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }
    }
}
