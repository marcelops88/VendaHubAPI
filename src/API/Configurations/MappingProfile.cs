using API.DTOs.Requests;
using AutoMapper;
using Domain.Entities;

namespace API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapProfile();
        }

        private void MapProfile()
        {
            CreateMap<VendaRequest, Venda>();
            CreateMap<ItemVendaRequest, ItemVenda>();
        }
    }
}
