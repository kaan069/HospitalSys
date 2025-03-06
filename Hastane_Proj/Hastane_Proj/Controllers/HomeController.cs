using Microsoft.AspNetCore.Mvc;
using Hastane_Proj.Models;
using System.Linq;
using Hastane_Proj.Data;
using Microsoft.EntityFrameworkCore;

namespace Hastane_Proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // TC ile sorgulama ekraný
        public IActionResult TCSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TCSearch(string tc)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.TcNo == tc);

            if (patient != null)
            {
                var patientDoctorRecords = _context.PatientDoctors
                    .Where(pd => pd.PatientId == patient.Id)
                    .Include(pd => pd.Doctor)  // Doktor bilgilerini al
                    .Include(pd => pd.Patient)  // Hasta bilgilerini al
                    .ToList();

                return View("TCSearch", patientDoctorRecords);
            }
            else
            {
                return View("TCSearch", new List<PatientDoctor>());  // Hasta bulunamazsa boþ liste gönder
            }
        }
    }
}
