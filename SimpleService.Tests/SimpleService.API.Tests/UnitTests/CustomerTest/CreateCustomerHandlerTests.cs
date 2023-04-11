using AutoMapper;
using Moq;
using SimpleService.API.Features.Customer.CQRS.Commands;
using SimpleService.API.Features.Customer.CQRS.Handlers;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Tests.UnitTests.CustomerTest
{
    public class CreateCustomerHandlerTests
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateCustomerHandlerTests() {
            _repositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CheckCreateCustomerAsyncIsCalled() {
            var customer = new CustomerModel { FirstName = "Test", LastName = "Test" };
            var customerRequest = new CreateCustomerRequest { FirstName = "Test", Surname = "Test" };
            _mapperMock.Setup(m => m.Map<CustomerModel>(customerRequest)).Returns(customer);

            var newCustomer = new CustomerModel { Id = 122, FirstName = customer.FirstName, LastName = customer.LastName };
            _repositoryMock.Setup(r => r.CreateCustomerAsync(customer)).Returns(Task.FromResult(newCustomer));

            var customerResult = new CustomerModelApi { Id = 12, FirstName = "Test", LastName = "Test" };
            _mapperMock.Setup(m => m.Map<CustomerModelApi>(newCustomer)).Returns(customerResult);

            // Act
            var createCustomerHandler = new CreateCustomerHandler(_repositoryMock.Object, _mapperMock.Object);
            var result = createCustomerHandler.Handle(new CreateCustomerCommand { Model = customerRequest }, new CancellationToken());

            // Assert
            _repositoryMock.Verify(r => r.CreateCustomerAsync(customer), Times.Once);
            Assert.NotNull(result.Result);
            Assert.Equal(result.Result, customerResult);
        }
    }
}