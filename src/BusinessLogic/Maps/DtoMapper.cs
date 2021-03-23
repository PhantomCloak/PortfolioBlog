using AutoMapper;
using Shared.Contracts.V1.Commands;
using Shared.DTOs;
using Shared.Models;

namespace BusinessLogic.Maps
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Content, ContentDto>()
                .ForMember(dest => dest.ContentName, opt => opt.MapFrom(src => src.ContentName))
                .ForMember(dest => dest.ContentFields, opt => opt.MapFrom(src => src.ContentFields));
            
            CreateMap<ContentDto,Content>()
                .ForMember(dest => dest.ContentName, opt => opt.MapFrom(src => src.ContentName))
                .ForMember(dest => dest.ContentFields, opt => opt.MapFrom(src => src.ContentFields));
            
            CreateMap<ContentCommand,Content>()
                .ForMember(dest => dest.ContentName, opt => opt.MapFrom(src => src.ContentName))
                .ForMember(dest => dest.ContentFields, opt => opt.MapFrom(src => src.ContentFields));

            CreateMap<Content,ContentCommand>()
                .ForMember(dest => dest.ContentName, opt => opt.MapFrom(src => src.ContentName))
                .ForMember(dest => dest.ContentFields, opt => opt.MapFrom(src => src.ContentFields));
        }
    }
}