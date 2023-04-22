using AutoMapper;
using Bravure.Entities;

namespace Bravure.Models.Patients
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<PatientDto, Patient>();
        }
    }
}
