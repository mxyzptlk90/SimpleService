using MediatR;
using SimpleService.API.Features.Customer.Models;
using SimpleService.Core.Customer.Models;

namespace SimpleService.API.Features.Customer.CQRS.Queries
{
    public class GetCustomerQuery : IRequest<CustomerModelApi>
    {
        public int Id { get; set; }
    }
}
