using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private readonly CarWorldContext _context;
        public FeedbackRepository(CarWorldContext context) : base(context)
        {
            _context = context;
        }
    }
}
