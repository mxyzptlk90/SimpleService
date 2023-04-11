using MediatR;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Features.Customer.CQRS.Commands
{
    public class CreateCustomerCommand: IRequest<CustomerModelApi>
    {
        public CreateCustomerRequest Model { get; set; }
    }
}
