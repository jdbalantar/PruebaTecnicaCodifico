using Api.Controllers.v1;
using Application.DTOs;
using Application.DTOs.Employee;
using Application.Features.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class EmployeesControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private EmployeesController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EmployeesController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var mediatorField = typeof(EmployeesController).BaseType?.GetField("_mediatorInstance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mediatorField?.SetValue(_controller, _mediatorMock.Object);
        }

        [Test]
        public async Task GetAllEmployees_ReturnsOkResult_WithEmployeeData()
        {
            var employees = new List<EmployeeDto>
            {
                new() { Empid = 1, Firstname = "John", Lastname = "Doe", Title = "Manager" },
                new() { Empid = 2, Firstname = "Jane", Lastname = "Smith", Title = "Developer" }
            };

            var expectedResult = Result<ICollection<EmployeeDto>>.Ok(employees);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllEmployeesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllEmployees() as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                var response = result.Value as Result<ICollection<EmployeeDto>>;
                Assert.That(response, Is.Not.Null);
                Assert.That(response?.IsSuccess, Is.True);
                Assert.That(response?.Data?.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetAllEmployees_ReturnsOkResult_WithEmptyList()
        {
            var expectedResult = Result<ICollection<EmployeeDto>>.Ok([]);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllEmployeesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllEmployees() as ObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                var response = result.Value as Result<ICollection<EmployeeDto>>;
                Assert.That(response, Is.Not.Null);
                Assert.That(actual: response?.IsSuccess, Is.True);
                Assert.That(response?.Data?.Count, Is.EqualTo(0));
            });
        }
    }
}
