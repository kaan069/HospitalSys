using System.ComponentModel.DataAnnotations;

namespace Hastane_Proj.Models
{
    public class Doctor
    {
        [Key] //birincil anahtar 
        public int Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C Kimlik Numarası 11 haneli olmalıdır.")]
        public string DoctorTc { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Deparment { get; set; } //doktor bölümü departmanı 
    }
}
