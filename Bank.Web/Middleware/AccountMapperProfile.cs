using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;

namespace Bank.Web.Middleware
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Accounts, AccountViewModel>();
            CreateMap<Accounts, AccountDetailsViewModel>();
        }
    }
}
