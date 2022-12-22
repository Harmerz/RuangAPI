namespace RuangAPI.Model
{
    [Table("login")]
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("username")]
        public string username { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("email")]
        public string email { get; set; }
        [Column("fullname")]
        public string fullname { get; set; }
        [Column("nim")]
        public string nim { get; set; }
    }
}
