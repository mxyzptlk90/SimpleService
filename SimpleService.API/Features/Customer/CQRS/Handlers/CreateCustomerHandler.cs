using AutoMapper;
using MediatR;
using SimpleService.API.Features.Customer.CQRS.Commands;
using SimpleService.API.Features.Customer.Models;
using SimpleService.Core.Customer.Interfaces;
using SimpleService.Core.Customer.Models;

namespace SimpleService.API.Features.Customer.CQRS.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerModelApi>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper) {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerModelApi> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) {
            var newCustomer = _mapper.Map<CustomerModel>(request.Model);
            newCustomer = await _customerRepository.CreateCustomerAsync(newCustomer);

            return _mapper.Map<CustomerModelApi>(newCustomer);
        }
    }
}
