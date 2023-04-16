using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Bravure.Entities.Abstractions;
using System.Linq;
namespace Bravure.Services;

public class MedicineService : IMedicineService
{
    private readonly BravureDbContext _context;

    public MedicineService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreateMedicine(Medicine medicine)
    {
        _context.Medicines.Add(medicine);
        _context.SaveChanges();
    }

    public Medicine GetMedicine(Guid id)
    {
        return _context.Medicines.Find(id);
    }

    public List<Medicine> GetAllMedicines()
    {
        return _context.Medicines.ToList();
    }

    public void UpdateMedicine(Medicine medicine)
    {
        // Validation logic, if any
        _context.Medicines.Update(medicine);
        _context.SaveChanges();
    }

    public void DeleteMedicine(Guid id)
    {
        var medicine = _context.Medicines.Find(id);
        if (medicine != null)
        {
            _context.Medicines.Remove(medicine);
            _context.SaveChanges();
        }
    }
}
