using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class CustOrder
{
    [Column("custid")]
    public int? Custid { get; set; }

    [Column("ordermonth", TypeName = "datetime")]
    public DateTime? Ordermonth { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }
}
