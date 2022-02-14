using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarWorldContext _context;
        public UnitOfWork(CarWorldContext context)
        {
            _context = context;
            BrandRepository = new BrandRepository(_context);
            UserRepository = new UserRepository(_context);
            InterestedBrandRepository = new InterestedBrandRepository(_context);
            CarModelRepository = new CarModelRepository(_context);
            AttributionRepository = new AttributionRepository(_context);
            GenerationAttributionRepository = new GenerationAttributionRepository(_context);
            GenerationRepository = new GenerationRepository(_context);
            EngineTypeRepository = new EngineTypeRepository(_context);
            AccessoryRepository = new AccessoryRepository(_context);
            ProposalRepository = new ProposalRepository(_context);            
            ContestEventRepository = new ContestEventRepository(_context);
            PrizeRepository = new PrizeRepository(_context);
            ContestPrizeRepository = new ContestPrizeRepository(_context);
            CERegisterRepository = new CERegisterRepository(_context);
            ExchangeRepository = new ExchangeRepository(_context);            
            ExchangeResponseRepository = new ExchangeResponseRepository(_context);
            PostRepository = new PostRepository(_context);
            FeedbackRepository = new FeedbackRepository(_context);
            CityRepository = new CityRepository(_context);
            DistrictRepository = new DistrictRepository(_context);
            WardRepository = new WardRepository(_context);
        }
        public IBrandRepository BrandRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IInterestedBrandRepository InterestedBrandRepository { get; private set; }
        public ICarModelRepository CarModelRepository { get; private set; }
        public IAttributionRepository AttributionRepository { get; private set; }
        public IGenerationAttributionRepository GenerationAttributionRepository { get; private set; }
        public IGenerationRepository GenerationRepository { get; private set; }
        public IEngineTypeRepository EngineTypeRepository { get; private set; }
        public IAccessoryRepository AccessoryRepository { get; private set; }
        public IProposalRepository ProposalRepository { get; private set; }      
        public IContestEventRepository ContestEventRepository { get; private set; }
        public IPrizeRepository PrizeRepository { get; private set; }
        public IContestPrizeRepository ContestPrizeRepository { get; private set; }
        public ICERegisterRepository CERegisterRepository { get; private set; }       
        public IExchangeRepository ExchangeRepository { get; private set; }
        public IExchangeAccDetailRepository ExchangeAccDetailRepository { get; private set; }
        public IExchangeCarDetailRepository ExchangeCarDetailRepository { get; private set; }
        public IExchangeResponseRepository ExchangeResponseRepository { get; private set; }
        public IPostRepository PostRepository { get; private set; }
        public IFeedbackRepository FeedbackRepository { get; private set; }
        public ICityRepository CityRepository { get; private set; }
        public IDistrictRepository DistrictRepository { get; private set; }
        public IWardRepository WardRepository { get; private set; }

        

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        
     
    }
}
