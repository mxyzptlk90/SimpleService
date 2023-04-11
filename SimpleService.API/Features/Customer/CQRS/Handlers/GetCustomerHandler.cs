using AutoMapper;
using MediatR;
using SimpleService.API.Features.Customer.CQRS.Queries;
using SimpleService.API.Features.Customer.Models;
using SimpleService.Core.Customer.Interfaces;

namespace SimpleService.API.Features.Customer.CQRS.Handlers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerModelApi>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper) {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerModelApi> Handle(GetCustomerQuery request, CancellationToken cancellationToken) {
            var customer = await _customerRepository.GetCustomerAsync(request.Id);
            return _mapper.Map<CustomerModelApi>(customer);
        }
    }
}
