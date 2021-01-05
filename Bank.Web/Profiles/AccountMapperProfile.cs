using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System.Linq;

namespace Bank.Web.Profiles
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Accounts, AccountViewModel>();
            CreateMap<Accounts, AccountDetailsViewModel>();
            CreateMap<Accounts, AccountDepositViewModel>();
            CreateMap<Accounts, AccountWithdrawalViewModel>();
            CreateMap<Accounts, AccountTransferViewModel>()
                .ForMember(dest => dest.FromAccountId, opt => opt.MapFrom(src => src.AccountId));
        }
    }
}
