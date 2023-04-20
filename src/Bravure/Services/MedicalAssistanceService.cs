using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bravure.Entities;
using Bravure.Exceptions;
using Bravure.Models.MedicalAssistances;

namespace Bravure.Services;

public class MedicalAssistanceService : IMedicalAssistanceService
{
    private readonly BravureDbContext _context;
    private readonly IMapper _mapper;

    public MedicalAssistanceService(BravureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void CreateMedicalAssistance(MedicalAssistanceDto dto)
    {
        if (_context.MedicalAssistances.Any(x => x.DisplayId == dto.DisplayId))
        {
            throw new BadRequestException("Mã dịch vũ tồn tại");
        }
        _context.MedicalAssistances.Add(_mapper.Map<MedicalAssistance>(dto));
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

    public void UpdateMedicalAssistance(MedicalAssistanceDto dto)
    {
        if (_context.MedicalAssistances.Any(x => x.DisplayId == dto.DisplayId && x.Id != dto.Id))
        {
            throw new BadRequestException("Mã dịch vũ tồn tại");
        }
        _context.MedicalAssistances.Update(_mapper.Map<MedicalAssistance>(dto));
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
