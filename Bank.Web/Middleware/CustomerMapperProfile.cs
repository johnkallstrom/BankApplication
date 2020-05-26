using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.SearchModels;
using Bank.Web.ViewModels;
using Microsoft.Azure.Search.Models;

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
                .ForMember(dest => dest.PhoneCountryCode, opt => opt.MapFrom(src => src.Telephonecountrycode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telephonenumber))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Zipcode));

            CreateMap<Customers, CustomerViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Givenname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname));

            CreateMap<CreateCustomerViewModel, Customers>()
                .ForMember(dest => dest.Streetaddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Telephonenumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Zipcode, dest => dest.MapFrom(src => src.PostalCode));

            CreateMap<Customers, EditCustomerViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Emailaddress))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Givenname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telephonenumber))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.PhoneCountryCode, opt => opt.MapFrom(src => src.Telephonecountrycode));

            CreateMap<EditCustomerViewModel, Customers>()
                .ForMember(dest => dest.Streetaddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Telephonenumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Telephonecountrycode, opt => opt.MapFrom(src => src.PhoneCountryCode));
        }
    }
}
