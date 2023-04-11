using MediatR;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Features.Customer.CQRS.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerModelApi>> { }
}
