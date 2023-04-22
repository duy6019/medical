using System;
using System.Collections.Generic;
using Bravure.Entities;
using Bravure.Models.Patients;

namespace Bravure.Services;

public interface IPatientService
{
    void CreatePatient(PatientDto patient);
    void DeletePatient(Guid id);
    List<Patient> GetAllPatients();
    Patient GetPatient(Guid id);
    void UpdatePatient(PatientDto patient);
}