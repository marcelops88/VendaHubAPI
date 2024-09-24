using API.DTOs.Requests;
using API.DTOs.Responses;
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

            CreateMap<Venda, VendaResponse>()
                .ForMember(dest => dest.ValorTotalVenda, opt => opt.MapFrom(src => src.CalcularValorTotal()))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

            CreateMap<ItemVenda, ItemVendaResponse>();
        }
    }
}
