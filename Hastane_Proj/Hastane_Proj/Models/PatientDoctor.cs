using Hastane_Proj.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastane_Proj.Models
{
    public class PatientDoctor
    {
        [Key]
        public int Id { get; set; } // Birincil anahtar (Primary Key)

        [Required]
        public int PatientId { get; set; } // Hasta ID'si

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; } // Hasta ile ilişkilendirme

        [Required]
        public int DoctorId { get; set; } // Doktor ID'si

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; } // Doktor ile ilişkilendirme
        public DateTime RecordDate { get; set; } = DateTime.Now; // Kayıt Tarihi
     
    }
}
