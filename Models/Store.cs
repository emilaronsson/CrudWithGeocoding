using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CrudWithGeocoding.Models
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Companies")]
        public Guid CompanyId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string Zip { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        [AllowNull]
        public string? Longitude { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        [AllowNull]
        public string? Latitude { get; set; }

    }
}
