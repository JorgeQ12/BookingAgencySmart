using Application.DTOs;
using Application.Interfaces.IServices.Administration;
using Application.Utilities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAgency.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }
        ///<summary>
        ///Buscar Usuario y Traer Token
        ///</summary>
        [HttpPost]
        [Route(nameof(LoginController.TokenByUser))]
        public async Task<ResultResponse<string>> TokenByUser(UserDTO user) => await _loginServices.TokenByUser(user);
    }
}
