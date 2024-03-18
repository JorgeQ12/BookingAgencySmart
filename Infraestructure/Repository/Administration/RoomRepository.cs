using Application.Interfaces.IRepository.Administration;
using Domain.Context;
using Domain.Models;
using Infraestructure.Repository.Common;

namespace Infraestructure.Repository.Administration
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
