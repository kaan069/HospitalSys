using System.ComponentModel.DataAnnotations;

namespace Hastane_Proj.Models
{
    public class Patient
    {
        [Key] //birinmcil anahtar
        public int Id { get; set; }
       
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C Kimlik Numarası 11 haneli olmalıdır.")]
        public string? TcNo { get; set; }
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Phone { get; set; }

    }
}
