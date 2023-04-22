using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bravure.Models.Examinations;
using Bravure.Exceptions;
using AutoMapper;
using Humanizer;

namespace Bravure.Services;

public class CilinicExaminationService : ICilinicExaminationService
{
    private readonly BravureDbContext _context;
    private readonly IMapper _mapper;

    public CilinicExaminationService(BravureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void CreateCilinicExamination(CilinicExaminationDto dto)
    {
        if(_context.CilinicExaminations.Any(x => x.DisplayID == dto.DisplayID))
        {
            throw new BadRequestException("Mã khám đã tồn tại!");
        }

        _context.CilinicExaminations.Add(_mapper.Map<CilinicExamination>(dto));
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

    public void UpdateCilinicExamination(CilinicExaminationDto dto)
    {
        if (_context.CilinicExaminations.Any(x => x.DisplayID == dto.DisplayID && x.Id != dto.Id))
        {
            throw new BadRequestException("Mã khám đã tồn tại!");
        }

        _context.CilinicExaminations.Update(_mapper.Map<CilinicExamination>(dto));
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
