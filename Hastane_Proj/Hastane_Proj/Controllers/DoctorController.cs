using Hastane_Proj.Data;
using Hastane_Proj.Models;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hastane_Proj.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Doktorları listeleyen sayfa
        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList(); // Tüm doktorları getir
            return View(doctors);
        }

        // Yeni doktor ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        // Yeni doktor ekleme işlemi (POST)
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor); // Yeni doktoru veri tabanına ekle
                _context.SaveChanges();
                return RedirectToAction("Index"); // Listeye geri dön
            }
            return View(doctor);
        }
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // Hasta güncelleme işlemi (POST)
        [HttpPost]
        public IActionResult Edit(int id, Doctor updatedDoctor)
        {
            if (ModelState.IsValid)
            {
                var doctor = _context.Doctors.Find(id);
                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.DoctorTc = updatedDoctor.DoctorTc;
                doctor.FirstName = updatedDoctor.FirstName;
                doctor.LastName = updatedDoctor.LastName;
                doctor.Deparment = updatedDoctor.Deparment;


                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updatedDoctor);
        }
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // T.C Kimlik Numarası ile Doktor arama
        public IActionResult Search(string DoctorTc)
        {
            if (string.IsNullOrEmpty(DoctorTc))
            {
                return View();
            }

            var doctor = _context.Doctors.FirstOrDefault(p => p.DoctorTc == DoctorTc);
            if (doctor == null)
            {
                ViewBag.Message = "Hasta bulunamadı.";
                return View();
            }

            return View(doctor);
        }
    }
}
