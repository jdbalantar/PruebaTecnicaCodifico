using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class OrderValue
{
    [Column("orderid")]
    public int Orderid { get; set; }

    [Column("custid")]
    public int? Custid { get; set; }

    [Column("empid")]
    public int Empid { get; set; }

    [Column("shipperid")]
    public int Shipperid { get; set; }

    [Column("orderdate", TypeName = "datetime")]
    public DateTime Orderdate { get; set; }

    [Column("val", TypeName = "numeric(12, 2)")]
    public decimal? Val { get; set; }
}
