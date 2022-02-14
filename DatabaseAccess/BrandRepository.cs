using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly CarWorldContext _context;
        public BrandRepository(CarWorldContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
