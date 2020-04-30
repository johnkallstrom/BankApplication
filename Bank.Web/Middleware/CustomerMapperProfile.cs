using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;

namespace Bank.Web.Middleware
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<Customers, CustomerProfileViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Emailaddress))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Givenname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telephonenumber))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Zipcode));
        }
    }
}
