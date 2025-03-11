namespace Application.DTOs.Product
{
    public class ProductDto
    {
        public int Productid { get; set; }

        public string Productname { get; set; } = null!;

        public int Supplierid { get; set; }

        public string? Category { get; set; } = string.Empty;

        public decimal Unitprice { get; set; }

        public bool Discontinued { get; set; }

    }
}
