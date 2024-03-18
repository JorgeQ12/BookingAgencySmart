using Application.DTOs;
using Application.Interfaces.IRepository.Administration;
using Application.Interfaces.IRepository.Common;
using Application.Interfaces.IServices.Administration;
using Application.Security;
using Application.Utilities;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using FluentValidation.Results;
using System.Text;

namespace Application.Services.Administration
{
    public class LoginServices : ILoginServices
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        public LoginServices(ILoginRepository loginRepository, IMapper mapper , JwtTokenGenerator jwtTokenGenerator)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ResultResponse<string>> TokenByUser(UserDTO user)
        {
            try
            {
                //Validar El DTO que este lleno
                UserDTOValidator validations = new UserDTOValidator();
                ValidationResult result = validations.Validate(user);
                if (!result.IsValid)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    result.Errors.ForEach(x => errorMessage.AppendLine($"{x.ErrorMessage}")); 
                    return new ResultResponse<string>(false, errorMessage.ToString());
                }

                IEnumerable<User> userExist = await _loginRepository.GetAll();
                User userCreate = userExist.Where(x => x.Username.ToLower().Equals(user.Username.ToLower()) && x.Password.ToLower().Equals(user.Password.ToLower())).FirstOrDefault();
                var tokenUser = await _jwtTokenGenerator.GenerateToken(userCreate);
                
                return new ResultResponse<string>(true, tokenUser.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
