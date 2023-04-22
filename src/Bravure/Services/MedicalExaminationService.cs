using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
using AutoMapper;
using Bravure.Models.Examinations;
using Bravure.Exceptions;

namespace Bravure.Services;

public class MedicalExaminationService : IMedicalExaminationService
{
    private readonly BravureDbContext _context;
    private readonly IMapper _mapper;

    public MedicalExaminationService(BravureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void CreateMedicalExamination(MedicalExaminationDto dto)
    {
        if (_context.MedicalExaminations.Any(x => x.DisplayID == dto.DisplayID))
        {
            throw new BadRequestException("Mã khám đã tồn tại!");
        }
        _context.MedicalExaminations.Add(_mapper.Map<MedicalExamination>(dto));
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

    public void UpdateMedicalExamination(MedicalExaminationDto dto)
    {
        if(_context.MedicalExaminations.Any(x => x.DisplayID == dto.DisplayID && x.Id != dto.Id))
        {
            throw new BadRequestException("Mã khám đã tồn tại");
        }
        _context.MedicalExaminations.Update(_mapper.Map<MedicalExamination>(dto));
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
