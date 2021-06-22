using AutoMapper;
using AzureAutoMapperFunction.Models;

namespace AzureAutoMapperFunction.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FunctionRequest, LegacySystemRequest>()
                .ForMember(dst => dst.CorrelationId, m => m.MapFrom(src => src.CorrelationId))
                .ForMember(dst => dst.Id, m => m.MapFrom(src => src.Reference));

            CreateMap<LegacySystemResponse, FunctionResponse>()
                .ForMember(dst => dst.Message, m => m.MapFrom(src => src.Message))
                .ForMember(dst => dst.CreatedAt, m => m.MapFrom(src => src.Timestamp))
                .ForMember(dst => dst.Success, m => m.MapFrom(src => src.ErrorCode == 0));
        }
    }
}