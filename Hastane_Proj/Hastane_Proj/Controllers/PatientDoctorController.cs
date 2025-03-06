using Hastane_Proj.Data;
using Hastane_Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Hastane_Proj.Controllers
{
    public class PatientDoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hasta-Doktor Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Patients"] = new SelectList(_context.Patients, "Id", "FirstName");
            ViewData["Doctors"] = new SelectList(_context.Doctors.Select(d => new
            {
                d.Id,
                FullName = d.FirstName + " " + d.LastName + " - " + d.Deparment
            }), "Id", "FullName");

            return View();
        }

        // Hasta-Doktor Ekleme (POST)
        [HttpPost]
        public IActionResult Create(PatientDoctor patientDoctor)
        {
            if (ModelState.IsValid)
            {
                _context.PatientDoctors.Add(patientDoctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Formu tekrar yüklerken dropdownları güncelliyoruz.
            ViewData["Patients"] = new SelectList(_context.Patients, "Id", "FirstName");
            ViewData["Doctors"] = new SelectList(_context.Doctors.Select(d => new
            {
                d.Id,
                FullName = d.FirstName + " " + d.LastName + " - " + d.Deparment
            }), "Id", "FullName");

            return View(patientDoctor);
        }

        // Hasta-Doktor ilişkisini listeleyen sayfa
        public IActionResult Index()
        {
            var patientDoctors = _context.PatientDoctors
                .Select(pd => new
                {
                    Id = pd.Id,
                    PatientName = _context.Patients
                        .Where(p => p.Id == pd.PatientId)
                        .Select(p => p.FirstName + " " + p.LastName)
                        .FirstOrDefault(),
                    DoctorName = _context.Doctors
                        .Where(d => d.Id == pd.DoctorId)
                        .Select(d => d.FirstName + " " + d.LastName)
                        .FirstOrDefault(),
                    Deparment = _context.Doctors
                        .Where(d => d.Id == pd.DoctorId)
                        .Select(d => d.Deparment)
                        .FirstOrDefault()
                })
                .ToList();

            return View(patientDoctors);
        }
        public IActionResult Delete(int id)
        {
            var patientDoctor = _context.PatientDoctors.Find(id);
            if (patientDoctor == null)
            {
                return NotFound();
            }

            _context.PatientDoctors.Remove(patientDoctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
