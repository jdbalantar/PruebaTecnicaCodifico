using Api.Controllers;
using Api.Controllers.v1;
using Application.DTOs;
using Application.DTOs.Order;
using Application.Features.Order.Commands;
using Application.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private OrdersController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new OrdersController();
            InjectMediator(_controller, _mediatorMock.Object);
        }

        #region GetOrdersByCustomer Tests

        [Test]
        public async Task GetOrdersByCustomer_ReturnsOk_WhenOrdersExist()
        {
            int customerId = 1;
            var orders = new List<OrderDto>
            {
                new() { Orderid = 1, Custid = customerId, Empid = 1, Shipname = "Test Ship" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOrdersByCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<ICollection<OrderDto>>.Ok(orders));

            var result = await _controller.GetOrdersByCustomer(customerId) as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(result.Value, Is.InstanceOf<Result<ICollection<OrderDto>>>());
                Assert.That(((Result<ICollection<OrderDto>>)result.Value!).IsSuccess, Is.True);
            });
        }

        [Test]
        public async Task GetOrdersByCustomer_ReturnsNotFound_WhenNoOrders()
        {
            int customerId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOrdersByCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<ICollection<OrderDto>>.NotFound());

            var result = await _controller.GetOrdersByCustomer(customerId) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        #endregion

        #region CreateOrder Tests

        [Test]
        public async Task CreateOrder_ReturnsCreated_WhenOrderIsValid()
        {
            var command = new CreateOrderCommand
            {
                Custid = 1,
                Empid = 2,
                Orderdate = DateTime.UtcNow,
                Requireddate = DateTime.UtcNow.AddDays(7),
                Shipperid = 1,
                Freight = 10.5m,
                Shipname = "Test Ship",
                Shipaddress = "123 Test St",
                Shipcity = "Test City",
                Shipcountry = "Test Country"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.Created(1));

            var result = await _controller.CreateOrder(command) as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
                Assert.That(result.Value, Is.InstanceOf<Result<int>>());
                Assert.That(((Result<int>)result.Value!).IsSuccess, Is.True);
            });
        }

        [Test]
        public async Task CreateOrder_ReturnsBadRequest_WhenValidationFails()
        {
            var command = new CreateOrderCommand(); // Datos inválidos

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.BadRequest("Validation failed"));

            var result = await _controller.CreateOrder(command) as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        #endregion

        #region Helper Methods

        private static void InjectMediator<T>(BaseApiController<T> controller, IMediator mediator) where T : class
        {
            typeof(BaseApiController<T>)
                .GetField("_mediatorInstance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(controller, mediator);
        }

        #endregion
    }
}
