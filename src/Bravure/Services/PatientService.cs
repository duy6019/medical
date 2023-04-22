using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
using Bravure.Models.Patients;
using Bravure.Exceptions;
using AutoMapper;

namespace Bravure.Services;

public class PatientService : IPatientService
{
    private readonly BravureDbContext _context;
    private readonly IMapper _mapper;

    public PatientService(BravureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void CreatePatient(PatientDto dto)
    {
        if (_context.Patients.Any(x => x.DisplayID == dto.DisplayID))
        {
            throw new BadRequestException("Mã bệnh nhân tồn tại");
        }

        _context.Patients.Add(_mapper.Map<Patient>(dto));
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

    public void UpdatePatient(PatientDto dto)
    {
        if(_context.Patients.Any(x => x.DisplayID == dto.DisplayID && x.Id != dto.Id))
        {
            throw new BadRequestException("Mã bệnh nhân tồn tại");
        }

        _context.Patients.Update(_mapper.Map<Patient>(dto));
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
