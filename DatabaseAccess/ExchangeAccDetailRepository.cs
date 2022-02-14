using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class ExchangeAccDetailRepository : Repository<ExchangeAccessorryDetail>, IExchangeAccDetailRepository
    {
        private readonly CarWorldContext _context;
        public ExchangeAccDetailRepository(CarWorldContext context) : base(context)
        {
            _context = context;
        }
    }
}
