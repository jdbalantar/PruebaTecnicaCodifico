using Application.DTOs.Customer;
using Application.DTOs.Employee;
using Application.DTOs.OrderDetail;

namespace Application.DTOs.Order
{
    public class OrderDto
    {
        public int Orderid { get; set; }

        public int? Custid { get; set; }

        public int Empid { get; set; }

        public DateTime Orderdate { get; set; }

        public DateTime Requireddate { get; set; }

        public DateTime? Shippeddate { get; set; }

        public int Shipperid { get; set; }

        public decimal Freight { get; set; }

        public string Shipname { get; set; } = null!;

        public string Shipaddress { get; set; } = null!;

        public string Shipcity { get; set; } = null!;

        public string? Shipregion { get; set; }

        public string? Shippostalcode { get; set; }

        public string Shipcountry { get; set; } = null!;

        public virtual CustomerDto? Cust { get; set; }

        public virtual EmployeeDto Emp { get; set; } = null!;

        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = [];

        public virtual Domain.Entities.Shipper Shipper { get; set; } = null!;
    }
}
