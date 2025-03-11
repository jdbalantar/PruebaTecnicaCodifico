using Api.Controllers.v1;
using Application.DTOs;
using Application.DTOs.Shipper;
using Application.Features.Shipper.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class ShippersControllerTests
    {
        private ShippersController _controller;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            var services = new ServiceCollection();
            services.AddSingleton(_mediatorMock.Object);
            var serviceProvider = services.BuildServiceProvider();
            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };
            _controller = new ShippersController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        [Test]
        public async Task GetAllShippers_ShouldReturnOkWithExpectedCollection()
        {
            var shippers = new List<ShipperDto>
            {
                new() { Shipperid = 1, Companyname = "Shipper 1", Phone = "111-111" },
                new() { Shipperid = 2, Companyname = "Shipper 2", Phone = "222-222" }
            };

            var expectedResult = Result<ICollection<ShipperDto>>.Ok(shippers);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllShippersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllShippers();
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            var value = okResult.Value as Result<ICollection<ShipperDto>>;
            Assert.That(value, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(value.IsSuccess, Is.True);
                Assert.That(value.Data, Has.Count.EqualTo(shippers.Count));
            });
            Assert.That(value.Data, Is.EqualTo(shippers));
        }
    }
}
