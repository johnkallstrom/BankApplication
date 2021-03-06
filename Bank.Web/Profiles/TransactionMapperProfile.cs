﻿using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;

namespace Bank.Web.Profiles
{
    public class TransactionMapperProfile : Profile
    {
        public TransactionMapperProfile()
        {
            CreateMap<Transactions, TransactionViewModel>();
        }
    }
}
