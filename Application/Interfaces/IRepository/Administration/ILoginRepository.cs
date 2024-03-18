using Domain.Models;
using Application.Interfaces.IRepository.Common;

namespace Application.Interfaces.IRepository.Administration
{
    public interface ILoginRepository : IGenericRepository<User>
    {
    }
}
