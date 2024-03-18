using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class HotelDTOValidator : AbstractValidator<HotelDTO>
    {
        public HotelDTOValidator()
        {
            RuleFor(hotel => hotel.Name).NotEmpty().WithMessage("Hotel name is required.");
            RuleFor(hotel => hotel.Description).NotEmpty().WithMessage("Hotel description is required.");
            RuleFor(hotel => hotel.City).NotEmpty().WithMessage("Hotel city is required.");
            RuleFor(hotel => hotel.Address).NotEmpty().WithMessage("Hotel address is required.");
            RuleFor(hotel => hotel.Commission).GreaterThan(0).WithMessage("Hotel commission must be greater than zero.");
        }
    }
}
