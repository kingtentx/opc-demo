using System.ComponentModel.DataAnnotations;

namespace OPC.Data
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}