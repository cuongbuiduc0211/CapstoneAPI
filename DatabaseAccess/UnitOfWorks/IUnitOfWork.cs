using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository BrandRepository { get; }
        IUserRepository UserRepository { get; }
        IInterestedBrandRepository InterestedBrandRepository { get; }
        ICarModelRepository CarModelRepository { get; }
        IAttributionRepository AttributionRepository { get; }
        IGenerationAttributionRepository GenerationAttributionRepository { get; }
        IGenerationRepository GenerationRepository { get; }
        IEngineTypeRepository EngineTypeRepository { get; }
        IAccessoryRepository AccessoryRepository { get; }
        IProposalRepository ProposalRepository { get; }
        IContestEventRepository ContestEventRepository { get; }
        IPrizeRepository PrizeRepository { get; }
        IContestPrizeRepository ContestPrizeRepository { get; }
        ICERegisterRepository CERegisterRepository { get; }
        IExchangeRepository ExchangeRepository { get; }
        IExchangeAccDetailRepository ExchangeAccDetailRepository { get; }
        IExchangeCarDetailRepository ExchangeCarDetailRepository { get; }
        IExchangeResponseRepository ExchangeResponseRepository { get; }
        IPostRepository PostRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        ICityRepository CityRepository { get; }
        IDistrictRepository DistrictRepository { get; }
        IWardRepository WardRepository { get; }
        Task SaveAsync();
    }
}
