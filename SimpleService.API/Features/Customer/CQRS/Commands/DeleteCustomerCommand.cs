using MediatR;

namespace SimpleService.API.Features.Customer.CQRS.Commands
{
    public class DeleteCustomerCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
