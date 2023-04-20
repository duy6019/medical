using AutoMapper;
using Bravure.Entities;

namespace Bravure.Models.MedicalAssistances
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MedicalAssistanceDto, MedicalAssistance>();
        }
    }
}
