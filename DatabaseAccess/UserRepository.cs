using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CarWorldContext _context;
        
        public UserRepository(CarWorldContext context) : base(context)
        {
            _context = context;
            
        }


      
    }
}
