using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class OrderTotalsByYear
{
    [Column("orderyear")]
    public int? Orderyear { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }
}
