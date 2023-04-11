using AutoMapper;
using Moq;
using SimpleService.API.Features.Customer.CQRS.Handlers;
using SimpleService.API.Features.Customer.CQRS.Queries;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Tests.UnitTests.CustomerTest
{
    public class GetCustomerHandlerTest
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetCustomerHandlerTest()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CheckGetCustomerReturnsObject()
        {
            int id = new Random().Next(0, 1000);
            var customer = new CustomerModel { Id = id };
            _repositoryMock.Setup(r => r.GetCustomerAsync(id)).Returns(Task.FromResult(customer));

            var customerResult = new CustomerModelApi { Id = id };
            _mapperMock.Setup(m => m.Map<CustomerModelApi>(customer)).Returns(customerResult);

            var getCustomerHandler = new GetCustomerHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = getCustomerHandler.Handle(new GetCustomerQuery { Id = id }, new CancellationToken()).Result;

            // Assert
            _repositoryMock.Verify(r => r.GetCustomerAsync(id), Times.Once);
            Assert.Equal(result.Id, customer.Id);
        }

        [Fact]
        public void CheckGetCustomerReturnsNull()
        {
            int id = new Random().Next(0, 1000);
            _repositoryMock.Setup(r => r.GetCustomerAsync(It.IsAny<int>())).Returns(Task.FromResult<CustomerModel>(null));
            _mapperMock.Setup(m => m.Map<CustomerModelApi>(null)).Returns<CustomerModelApi>(null);
            var getCustomerHandler = new GetCustomerHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = getCustomerHandler.Handle(new GetCustomerQuery { Id = id }, new CancellationToken()).Result;

            // Assert
            _repositoryMock.Verify(r => r.GetCustomerAsync(id), Times.Once);
            Assert.Null(result);
        }
    }
}