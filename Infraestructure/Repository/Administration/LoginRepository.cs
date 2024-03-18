using Application.Interfaces.IRepository.Administration;
using Domain.Context;
using Domain.Models;
using Infraestructure.Repository.Common;

namespace Infraestructure.Repository.Administration
{
    public class LoginRepository : GenericRepository<User>, ILoginRepository
    {
        public readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
