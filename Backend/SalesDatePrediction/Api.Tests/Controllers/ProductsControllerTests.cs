using Api.Controllers;
using Api.Controllers.v1;
using Application.DTOs;
using Application.DTOs.Product;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController();
            InjectMediator(_controller, _mediatorMock.Object);
        }

        [Test]
        public async Task GetAllProducts_ReturnsOk_WhenProductsExist()
        {
            var products = new List<ProductDto>
            {
                new() { Productid = 1, Productname = "Laptop", Supplierid = 100, Category = "Electronics", Unitprice = 1200.99m, Discontinued = false },
                new() { Productid = 2, Productname = "Phone", Supplierid = 101, Category = "Electronics", Unitprice = 899.50m, Discontinued = false }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<ICollection<ProductDto>>.Ok(products));

            var result = await _controller.GetAllProducts() as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(result.Value, Is.InstanceOf<Result<ICollection<ProductDto>>>());
            });

            var response = result.Value as Result<ICollection<ProductDto>>;
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Data?.Count, Is.EqualTo(2));
            });

            var productList = response.Data?.ToList();
            Assert.Multiple(() =>
            {
                Assert.That(productList?[0].Productname, Is.EqualTo("Laptop"));
                Assert.That(productList?[0].Supplierid, Is.EqualTo(100));
                Assert.That(productList?[0].Category, Is.EqualTo("Electronics"));
                Assert.That(productList?[0].Unitprice, Is.EqualTo(1200.99m));
                Assert.That(productList?[0].Discontinued, Is.False);
            });
        }

        [Test]
        public async Task GetAllProducts_ReturnsOk_WhenNoProductsExist()
        {
            var emptyProducts = new List<ProductDto>();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<ICollection<ProductDto>>.Ok(emptyProducts));

            var result = await _controller.GetAllProducts() as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(result.Value, Is.InstanceOf<Result<ICollection<ProductDto>>>());
            });

            var response = result.Value as Result<ICollection<ProductDto>>;
            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Data?.Count, Is.EqualTo(0));
            });
        }

        private static void InjectMediator<T>(BaseApiController<T> controller, IMediator mediator) where T : class
        {
            typeof(BaseApiController<T>)
                .GetField("_mediatorInstance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(controller, mediator);
        }
    }
}
