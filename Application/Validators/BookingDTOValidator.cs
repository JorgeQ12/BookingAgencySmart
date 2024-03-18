using Application.DTOs;
using Domain.Models;
using FluentValidation;

namespace Application.Validators
{
    public class BookingDTOValidator : AbstractValidator<BookingDTO>
    {
        public BookingDTOValidator()
        {
            RuleFor(booking => booking.RoomId).NotEmpty().WithMessage("Room ID is required.");

            RuleFor(booking => booking.CheckInDate)
                .NotEmpty().WithMessage("Check-in date is required.")
                .Must(date => IsValidDate(date)).WithMessage("Check-in date must be a valid date format.");

            RuleFor(booking => booking.CheckOutDate)
                .NotEmpty().WithMessage("Check-out date is required.")
                .Must((booking, checkout) => IsCheckOutValid(booking.CheckInDate, checkout))
                .WithMessage("Check-out date must be greater than or equal to the check-in date.")
                .Must(date => IsValidDate(date)).WithMessage("Check-out date must be a valid date format.");

            RuleFor(booking => booking.ContactBooking).SetValidator(new ContactBookingDTOValidator());
            RuleForEach(booking => booking.Guests).SetValidator(new GuestsBookingDTOValidator());
        }

        private bool IsValidDate(DateTime date)
        {
            return date != default && date != DateTime.MinValue;
        }

        private bool IsCheckOutValid(DateTime checkInDate, DateTime checkOutDate)
        {
            return checkOutDate >= checkInDate;
        }
    }
    public class ContactBookingDTOValidator : AbstractValidator<ContactBookingDTO>
    {
        public ContactBookingDTOValidator()
        {
            RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("Contact name is required.");
            RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Contact phone number is required.");
        }
    }

    public class GuestsBookingDTOValidator : AbstractValidator<GuestsBookingDTO>
    {
        public GuestsBookingDTOValidator()
        {
            RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("Guest name is required.");
            RuleFor(dto => dto.LastName).NotEmpty().WithMessage("Guest last name is required.");
            RuleFor(dto => dto.BirthDate).NotEmpty().WithMessage("Guest birth date is required.");
            RuleFor(dto => dto.Gender).NotEmpty().WithMessage("Guest gender is required.");
            RuleFor(dto => dto.DocumentType).NotEmpty().WithMessage("Guest document type is required.");
            RuleFor(dto => dto.DocumentNumber).NotEmpty().WithMessage("Guest document number is required.");
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Guest email is invalid.");
            RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Guest phone number is required.");
        }
    }
}
