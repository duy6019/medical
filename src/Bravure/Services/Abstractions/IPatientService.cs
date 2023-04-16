using System;
using System.Collections.Generic;
using Bravure.Entities;
namespace Bravure.Services;

public interface IPatientService
{
    void CreatePatient(Patient patient);
    void DeletePatient(Guid id);
    List<Patient> GetAllPatients();
    Patient GetPatient(Guid id);
    void UpdatePatient(Patient patient);
}