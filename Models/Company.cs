using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CrudWithGeocoding.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        public string Name { get; set; }

        public int OrganizationNumber { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [AllowNull]
        public string? Notes { get; set; }

        public virtual List<Store> Stores { get; set; } = new();
    }
}
