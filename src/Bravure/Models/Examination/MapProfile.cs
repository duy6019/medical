using AutoMapper;
using Bravure.Entities;

namespace Bravure.Models.Examinations
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MedicalExaminationDto, MedicalExamination>();
            CreateMap<CilinicExaminationDto, CilinicExamination>();
        }
    }
}
