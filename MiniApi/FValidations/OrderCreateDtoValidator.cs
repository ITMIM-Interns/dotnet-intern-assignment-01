using FluentValidation;
using MiniApi.Dtos.Orders;

namespace MiniApi.FValidations
{
    public  class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(o => o.CustomerName).NotEmpty().WithMessage("Customer name cannot be empty");
            RuleFor(o => o.ProductName).NotEmpty().WithMessage("Product name cannot be empty");
            RuleFor(o => o.Quantity).GreaterThan(0).WithMessage("Quantity greather than zero");
            RuleFor(o => o.CurrentPrice).GreaterThan(0).WithMessage("Price greather than zero");

        }
    }
}
