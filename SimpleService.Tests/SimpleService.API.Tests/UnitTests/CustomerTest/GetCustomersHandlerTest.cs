using AutoMapper;
using Moq;
using SimpleService.API.Features.Customer.CQRS.Handlers;
using SimpleService.API.Features.Customer.CQRS.Queries;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Tests.UnitTests.CustomerTest
{
    public class GetCustomersHandlerTest
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetCustomersHandlerTest() {
            _repositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CheckGetCustomersIsCalled() {
            var customerList = new List<CustomerModel> { new CustomerModel() };
            _repositoryMock.Setup(r => r.GetCustomersAsync()).Returns(Task.FromResult(customerList.AsEnumerable()));

            var customerListResult = new List<CustomerModelApi> { new CustomerModelApi() };
            _mapperMock.Setup(m => m.Map<IEnumerable<CustomerModelApi>>(customerList)).Returns(customerListResult);

            var getCustomersHandler = new GetCustomersHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = getCustomersHandler.Handle(new GetCustomersQuery(), new CancellationToken()).Result;

            // Assert
            _repositoryMock.Verify(r => r.GetCustomersAsync(), Times.Once);
            Assert.Equal(result, customerListResult);
        }
    }
}