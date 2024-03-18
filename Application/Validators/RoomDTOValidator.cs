using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RoomDTOValidator : AbstractValidator<RoomDTO>
    {
        public RoomDTOValidator()
        {
            RuleFor(room => room.HotelId).NotEmpty().WithMessage("Hotel ID is required.");
            RuleFor(room => room.NumberPeople).GreaterThan(0).WithMessage("Number of people must be greater than zero.");
            RuleFor(room => room.Location).NotEmpty().WithMessage("Room location is required.");
            RuleFor(room => room.Type).IsInEnum().WithMessage("Invalid room type.");
            RuleFor(room => room.BaseCost).GreaterThan(0).WithMessage("Room base cost must be greater than zero.");
            RuleFor(room => room.Taxes).GreaterThan(0).WithMessage("Room taxes must be greater than zero.");
        }
    }
}
