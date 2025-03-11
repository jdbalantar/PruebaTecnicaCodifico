using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Employee
{
    public class EmployeeDto
    {
        [Key]
        [Column("empid")]
        public int Empid { get; set; }

        [Column("lastname")]
        [StringLength(20)]
        public string Lastname { get; set; } = null!;

        [Column("firstname")]
        [StringLength(10)]
        public string Firstname { get; set; } = null!;

        [Column("title")]
        [StringLength(30)]
        public string Title { get; set; } = null!;

        [Column("titleofcourtesy")]
        [StringLength(25)]
        public string Titleofcourtesy { get; set; } = null!;

        [Column("birthdate", TypeName = "datetime")]
        public DateTime Birthdate { get; set; }

        [Column("hiredate", TypeName = "datetime")]
        public DateTime Hiredate { get; set; }

        [Column("address")]
        [StringLength(60)]
        public string Address { get; set; } = null!;

        [Column("city")]
        [StringLength(15)]
        public string City { get; set; } = null!;

        [Column("region")]
        [StringLength(15)]
        public string? Region { get; set; }

        [Column("postalcode")]
        [StringLength(10)]
        public string? Postalcode { get; set; }

        [Column("country")]
        [StringLength(15)]
        public string Country { get; set; } = null!;

        [Column("phone")]
        [StringLength(24)]
        public string Phone { get; set; } = null!;

        [Column("mgrid")]
        public int? Mgrid { get; set; }
    }
}
