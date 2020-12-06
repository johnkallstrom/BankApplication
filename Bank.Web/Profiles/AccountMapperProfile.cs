using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;

namespace Bank.Web.Profiles
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Accounts, AccountViewModel>();
            CreateMap<Accounts, AccountDetailsViewModel>();
            CreateMap<Accounts, CreateDepositViewModel>();
            CreateMap<Accounts, CreateWithdrawalViewModel>();
            CreateMap<Accounts, CreateTransferViewModel>()
                .ForMember(dest => dest.FromAccountId, opt => opt.MapFrom(src => src.AccountId));
        }
    }
}
