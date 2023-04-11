using AutoMapper;
using MediatR;
using SimpleService.API.Features.Customer.CQRS.Queries;
using SimpleService.API.Features.Customer.Models;
using SimpleService.Core.Customer.Interfaces;

namespace SimpleService.API.Features.Customer.CQRS.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerModelApi>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersHandler(ICustomerRepository customerRepository, IMapper mapper) {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerModelApi>> Handle(GetCustomersQuery request, CancellationToken cancellationToken) 
                            => _mapper.Map<IEnumerable<CustomerModelApi>>(await _customerRepository.GetCustomersAsync());
    }
}
