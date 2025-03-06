using Microsoft.EntityFrameworkCore;
using Hastane_Proj.Models;

namespace Hastane_Proj.Data
{
    public class ApplicationDbContext : DbContext
    {
        // DbContextOptions üzerinden dbContext yapılandırılır
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Veritabanındaki tablolar
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientDoctor> PatientDoctors { get; set; }
    }
}
