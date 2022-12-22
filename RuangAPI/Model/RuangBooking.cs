using System.ComponentModel.DataAnnotations;

namespace RuangAPI.Model
{
    [Table("ruangbooking")]
    public class RuangBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int? Id { get; set; }
        [Column("name")]
        public string? name { get; set; }
        [Column("date")]
        public string? date { get; set; }
        [Column("room")]
        public string? room { get; set; }
        [Column("NIM")]
        public string? nim { get; set; }
        [Column("_00")]
        public string? _00 { get; set; }
        [Column("_01")]
        public string? _01 { get; set; }
        [Column("_02")]
        public string? _02 { get; set; }
        [Column("purpose")]
        public string? purpose { get; set; }
        [Column("person")]
        public int? person { get; set; }

    }
}
