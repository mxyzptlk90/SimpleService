using MediatR;
using SimpleService.API.Features.Customer.CQRS.Commands;
using SimpleService.Core.Customer.Interfaces;

namespace SimpleService.API.Features.Customer.CQRS.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerHandler(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) 
                        => await _customerRepository.DeleteCustomerAsync(request.Id);
    }
}
