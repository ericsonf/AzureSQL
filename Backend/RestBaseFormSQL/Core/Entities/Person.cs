using System.ComponentModel.DataAnnotations;

namespace RestBaseFormSQL.Core.Entities
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(160)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(8), MaxLength(160)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public byte[] Picture { get; set; }
    }
}