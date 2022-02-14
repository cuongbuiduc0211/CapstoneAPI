using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class ExchangeResponseRepository : Repository<ExchangeResponse>, IExchangeResponseRepository

    {
        private readonly CarWorldContext _context;
        public ExchangeResponseRepository(CarWorldContext context) : base(context)
        {
            _context = context;
        }
    }
}
