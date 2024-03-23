using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using CleanNow.Infrastructured.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Persistence.Repository
{
    public class LikesRepository : GeneryRepository<Like>, ILikesRepository
    {
        private readonly ApplicationContext _context;

        public LikesRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
