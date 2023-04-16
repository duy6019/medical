using System;
using System.Collections.Generic;
using Bravure.Entities;

namespace Bravure.Services
{
    public interface IMedicineService
    {
        void CreateMedicine(Medicine medicine);
        void DeleteMedicine(Guid id);
        List<Medicine> GetAllMedicines();
        Medicine GetMedicine(Guid id);
        void UpdateMedicine(Medicine medicine);
    }
}