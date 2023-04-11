using Moq;
using SimpleService.API.Features.Customer.CQRS.Commands;
using SimpleService.API.Features.Customer.CQRS.Handlers;

namespace SimpleService.API.Tests.UnitTests.CustomerTest
{
    public class DeleteCustomerHandlerTests
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;

        public DeleteCustomerHandlerTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public void CheckDeleteCustomerMethodIsCalled()
        {
            int id = new Random().Next(0, 1000);
            var deleteCustomerHandler = new DeleteCustomerHandler(_repositoryMock.Object);

            var result = deleteCustomerHandler.Handle(new DeleteCustomerCommand { Id = id }, new CancellationToken());

            _repositoryMock.Verify(r => r.DeleteCustomerAsync(id), Times.Once);
        }
    }
}