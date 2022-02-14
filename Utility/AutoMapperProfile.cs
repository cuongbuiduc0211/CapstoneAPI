
using AutoMapper;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserAccount, User>();
            CreateMap<AdminAccount, User>();
            CreateMap<NewAccount, User>();
            CreateMap<SelfProfile, User>();
            CreateMap<ChangePassword, User>();
            CreateMap<BrandItem, Brand>();
            CreateMap<CarItem, CarModel>();
            CreateMap<AttributionItem, Attribution>();
            CreateMap<GenerationItem, Generation>();
            CreateMap<AccessoryItem, Accessory>();
            CreateMap<ProposalItem, Proposal>();
            CreateMap<ContestEventItem, ContestEvent>();
            CreateMap<CERegister, ContestEventRegister>();
            CreateMap<CheckInItem, ContestEventRegister>();
            CreateMap<PrizeItem, Prize>();
            CreateMap<ExchangeCarItem, Exchange>();
            CreateMap<ExchangeCarDetails, ExchangeCarDetail>();
            CreateMap<ExchangeAccItem, Exchange>();
            CreateMap<ExchangeAccessorryDetails, ExchangeAccessorryDetail>();
            CreateMap<ExchangeResItem, ExchangeResponse>();
            CreateMap<PostItem, Post>();
            CreateMap<ContestPrizeItem, ContestPrize>();
        }
        
    }
}
