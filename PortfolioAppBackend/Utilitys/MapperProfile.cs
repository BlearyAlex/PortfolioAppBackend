using AutoMapper;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Models;

namespace PortfolioAppBackend.Utilitys
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Image, ImageDTO>().ReverseMap();
        }
    }
}
