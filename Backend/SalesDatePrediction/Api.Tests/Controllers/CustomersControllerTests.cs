using Api.Controllers.v1;
using Application.DTOs;
using Application.DTOs.Customer;
using Application.Features.Customer.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CustomersController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var mediatorField = typeof(CustomersController).BaseType?.GetField("_mediatorInstance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mediatorField?.SetValue(_controller, _mediatorMock.Object);
        }

        [Test]
        public async Task GetCustomersWithOrderHistory_ReturnsOkResult_WithCustomerData()
        {
            var customerData = new List<CustomerWithOrderHistoryDto>
            {
                new() { Custid = 1, Companyname = "Test Company", Contactname = "John Doe" },
                new() { Custid = 2, Companyname = "Another Test Company", Contactname = "Jane Doe" }
            };

            var expectedResult = Result<ICollection<CustomerWithOrderHistoryDto>>.Ok(customerData);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCustomersWithOrderHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetCustomersWithOrderHistory() as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            var response = result.Value as Result<ICollection<CustomerWithOrderHistoryDto>>;
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Data, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public async Task GetCustomersWithOrderHistory_ReturnsOkResult_WithEmptyList()
        {
            var expectedResult = Result<ICollection<CustomerWithOrderHistoryDto>>.Ok([]);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCustomersWithOrderHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetCustomersWithOrderHistory() as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            var response = result.Value as Result<ICollection<CustomerWithOrderHistoryDto>>;
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Data?.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void GetCustomersWithOrderHistory_ThrowsException_OnMediatorFailure()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCustomersWithOrderHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database connection failed"));

            var ex = Assert.ThrowsAsync<Exception>(async () => await _controller.GetCustomersWithOrderHistory());

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.Contain("Database connection failed"));
        }

    }
}
