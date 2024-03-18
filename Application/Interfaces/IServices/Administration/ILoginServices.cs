using Application.DTOs;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Administration
{
    public interface ILoginServices
    {
        Task<ResultResponse<string>> TokenByUser(UserDTO user);
    }
}
